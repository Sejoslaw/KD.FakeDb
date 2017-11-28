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
        protected IList<IFakeTable> tables = new List<IFakeTable>();

        public virtual IFakeTable this[string tableName]
        {
            get
            {
                return GetTable(tableName);
            }
        }

        public virtual IFakeTable this[int tableIndex]
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

        public virtual IFakeTable AddTable(string tableName)
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

        public virtual IFakeTable GetTable(string tableName)
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

        public virtual void RemoveTable(string tableName)
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

        public virtual IEnumerator<IFakeTable> GetEnumerator()
        {
            return this.tables.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public virtual void Add(IFakeTable item)
        {
            this.tables.Add(item);
        }

        public virtual void Clear()
        {
            this.tables.Clear();
        }

        public virtual bool Contains(IFakeTable item)
        {
            return this.tables.Contains(item);
        }

        public virtual void CopyTo(IFakeTable[] array, int arrayIndex)
        {
            this.tables.CopyTo(array, arrayIndex);
        }

        public virtual bool Remove(IFakeTable item)
        {
            return this.tables.Remove(item);
        }
    }
}