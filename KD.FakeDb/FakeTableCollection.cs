using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace KD.FakeDb
{
    /// <summary>
    /// Default implementation of <see cref="IFakeTableCollection"/>.
    /// </summary>
    public class FakeTableCollection : IFakeTableCollection
    {
        /// <summary>
        /// For internal <see cref="IFakeTable"/>'s storing.
        /// </summary>
        private ISet<IFakeTable> tables = new HashSet<IFakeTable>();

        public IFakeTable this[string tableName]
        {
            get
            {
                return GetTable(tableName);
            }
        }

        public IFakeTable this[int tableIndex]
        {
            get
            {
                return this.tables.ElementAt(tableIndex);
            }
        }

        public int Count
        {
            get
            {
                return tables.Count;
            }
        }

        public IFakeDatabase Database { get; }

        public bool IsReadOnly
        {
            get
            {
                return this.tables.IsReadOnly;
            }
        }

        public FakeTableCollection(IFakeDatabase fakeDatabase)
        {
            this.Database = fakeDatabase;
        }

        public IFakeTable AddTable(string tableName)
        {
            var tabs = (from tab in this.tables
                        where tab.Name.Equals(tableName)
                        select tab);

            if (tabs.Any())
            {
                throw new Exception(string.Format("Table with name \"{0}\" already exists.", tableName));
            }

            var newTable = new FakeTable(this.Database, tableName);
            this.tables.Add(newTable);
            return newTable;
        }

        public IFakeTable GetTable(string tableName)
        {
            var tabs = (from tab in this.tables
                        where tab.Name.Equals(tableName)
                        select tab);

            if (tabs.Count() != 1)
            {
                throw new Exception(string.Format("There is none or multiple Tables with name \"{0}\"", tableName));
            }

            return tabs.First();
        }

        public void RemoveTable(string tableName)
        {
            var tabs = (from tab in this.tables
                        where tab.Name.Equals(tableName)
                        select tab);

            if (tabs.Count() != 1)
            {
                throw new Exception(string.Format("There is none or multiple Tables with name \"{0}\"", tableName));
            }

            tables.Remove(tabs.First());
        }

        public IEnumerator<IFakeTable> GetEnumerator()
        {
            return this.tables.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public void Add(IFakeTable item)
        {
            this.tables.Add(item);
        }

        public void Clear()
        {
            this.tables.Clear();
        }

        public bool Contains(IFakeTable item)
        {
            return this.tables.Contains(item);
        }

        public void CopyTo(IFakeTable[] array, int arrayIndex)
        {
            this.tables.CopyTo(array, arrayIndex);
        }

        public bool Remove(IFakeTable item)
        {
            return this.tables.Remove(item);
        }
    }
}