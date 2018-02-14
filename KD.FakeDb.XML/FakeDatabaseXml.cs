using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace KD.FakeDb.XML
{
    /// <summary>
    /// Basic implementation of <see cref="IFakeDatabaseXml"/>
    /// </summary>
    public class FakeDatabaseXml : IFakeDatabaseXml
    {
        private string DirectoryPath { get; set; }

        public DirectoryInfo DatabaseDirectory
        {
            get
            {
                return new DirectoryInfo(this.DirectoryPath);
            }
        }
        public string Name
        {
            get
            {
                string dirName = this.DatabaseDirectory.Name;
                return dirName;
            }
            set // Change Database Name == Change Folder Name
            {
                DirectoryInfo currentDbDir = this.DatabaseDirectory;
                DirectoryInfo parentDir = currentDbDir.Parent;
                string parentPath = parentDir.FullName;
                string newPath = Path.Combine(parentPath, value);
                Directory.Move(currentDbDir.FullName, newPath);
                this.DirectoryPath = newPath;

                this.CheckIfDatabaseDirectoryExists();
            }
        }
        public IEnumerable<IFakeTable> Tables
        {
            get // TODO: Remove hardcoded "xml" as extension, to support different extension types if they are xml-like.
            {
                foreach (FileInfo fi in this.DatabaseDirectory.GetFiles())
                {
                    if (fi.Extension.ToLower().Equals("xml"))
                    {
                        yield return new FakeTableXml(this, fi.Name);
                    }
                }
            }
        }
        public int Count
        {
            get
            {
                int count = this.Tables.Count();
                return count;
            }
        }
        public bool IsReadOnly
        {
            get // TODO: Add Xml-readonly mechanics.
            {
                return false;
            }
        }

        public IFakeTable this[string tableName]
        {
            get
            {
                IFakeTable table = this.GetTable(tableName);
                return table;
            }
            set
            {
                this.RemoveTable(tableName);
                this.Add(value);
            }
        }

        public FakeDatabaseXml(string path)
        {
            this.DirectoryPath = path;

            this.CheckIfDatabaseDirectoryExists();
        }

        public IFakeTable AddTable(string tableName)
        {
            this.Add(new FakeTableXml(this, tableName));
            IFakeTable table = this[tableName];
            return table;
        }

        public void RemoveTable(string tableName)
        {
            FileInfo tableFile = this.DatabaseDirectory.GetFiles().Where(file => file.Name.Equals(tableName)).FirstOrDefault();

            if (tableFile != null)
            {
                tableFile.Delete();
            }
            else
            {
                throw new ArgumentException($"Table with name: [{ tableName }] does not exist.", "tableName");
            }
        }

        public IFakeTable GetTable(string tableName)
        {
            IFakeTable table = this.Tables.Where(tab => tab.Name.Equals(tableName)).FirstOrDefault();
            return table;
        }

        public void Add(IFakeTable item)
        {
            string xml = XmlUtils.Parse(item);
            string path = Path.Combine(this.DirectoryPath, $"{ item.Name }.xml");

            using (StreamWriter writer = File.CreateText(path))
            {
                writer.Write(xml);
                writer.Flush();
                writer.Close();
            }
        }

        public void Clear()
        {
            this.DatabaseDirectory.GetFiles().ToList().ForEach(file => file.Delete());
        }

        public bool Contains(IFakeTable item)
        {
            bool ret = this.Tables.Where(file => file.Name.Equals(item.Name)).Any();
            return ret;
        }

        public void CopyTo(IFakeTable[] array, int arrayIndex)
        {
            if (array.Length != this.Tables.Count())
            {
                throw new ArgumentException($"Can't copy Tables to array. Array size is different than current number of Tables.", "array");
            }

            for (int i = 0; i < array.Length; ++i)
            {
                array[i] = this.Tables.ElementAt(i);
            }
        }

        public bool Remove(IFakeTable item)
        {
            this.RemoveTable(item.Name);
            return true;
        }

        public IEnumerator<IFakeTable> GetEnumerator()
        {
            throw new InvalidOperationException(@"You cannot directly enumerate over IFakeDatabaseXml. Instead enumerate over IFakeDatabaseXml.Tables.");
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private void CheckIfDatabaseDirectoryExists()
        {
            DirectoryInfo dirInf = this.DatabaseDirectory;
            if (!dirInf.Exists)
            {
                dirInf.Create();
            }
        }
    }
}