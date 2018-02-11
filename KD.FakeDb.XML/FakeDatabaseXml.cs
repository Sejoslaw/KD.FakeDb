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
                        yield return new FakeTableXml(this, fi);
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

        public IFakeTable this[string tableName] { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public FakeDatabaseXml(string path)
        {
            this.DirectoryPath = path;

            this.CheckIfDatabaseDirectoryExists();
        }

        public IFakeTable AddTable(string tableName)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveTable(string tableName)
        {
            throw new System.NotImplementedException();
        }

        public IFakeTable GetTable(string tableName)
        {
            throw new System.NotImplementedException();
        }

        public void Add(IFakeTable item)
        {
            throw new System.NotImplementedException();
        }

        public void Clear()
        {
            throw new System.NotImplementedException();
        }

        public bool Contains(IFakeTable item)
        {
            throw new System.NotImplementedException();
        }

        public void CopyTo(IFakeTable[] array, int arrayIndex)
        {
            throw new System.NotImplementedException();
        }

        public bool Remove(IFakeTable item)
        {
            throw new System.NotImplementedException();
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