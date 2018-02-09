using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace KD.FakeDb
{
    /// <summary>
    /// Default implementaion of <see cref="IFakeDatabase"/>.
    /// </summary>
    public class FakeDatabase : IFakeDatabase
    {
        public virtual IFakeTable this[string tableName]
        {
            get
            {
                IFakeTable table = this.Tables.Where(tab => tab.Name.Equals(tableName)).FirstOrDefault();
                return table;
            }
            set
            {
                this.AddTable(tableName);
            }
        }

        public string Name { get; set; }
        public IEnumerable<IFakeTable> Tables { get; protected set; }

        public int Count
        {
            get
            {
                return this.Tables.Count();
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public FakeDatabase()
        {
            this.Tables = new HashSet<IFakeTable>();
        }

        public virtual IFakeTable AddTable(string tableName)
        {
            var tabs = (from tab in this.Tables
                        where tab.Name.Equals(tableName)
                        select tab).ToList();

            if (tabs.Any())
            {
                throw new Exception($"Table with name \"{ tableName }\" already exists.");
            }

            var newTable = this.NewTable(tableName);
            this.Add(newTable);
            return newTable;
        }

        public virtual IFakeTable GetTable(string tableName)
        {
            var tabs = (from tab in this.Tables
                        where tab.Name.Equals(tableName)
                        select tab).ToList();

            if (tabs.Count() > 1)
            {
                throw new Exception($"There is none or multiple Tables with name \"{ tableName }\"");
            }

            return tabs.FirstOrDefault();
        }

        public virtual void RemoveTable(string tableName)
        {
            var tabs = (from tab in this.Tables
                        where tab.Name.Equals(tableName)
                        select tab).ToList();

            if (tabs.Count() > 1)
            {
                throw new Exception($"There is none or multiple Tables with name \"{ tableName }\"");
            }

            this.Remove(tabs.FirstOrDefault());
        }

        public virtual IEnumerator<IFakeTable> GetEnumerator()
        {
            return this.Tables.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public virtual void Add(IFakeTable item)
        {
            this.Cast().Add(item);
        }

        public virtual void Clear()
        {
            this.Cast().Clear();
        }

        public virtual bool Contains(IFakeTable item)
        {
            return this.Cast().Contains(item);
        }

        public virtual void CopyTo(IFakeTable[] array, int arrayIndex)
        {
            this.Cast().CopyTo(array, arrayIndex);
        }

        public virtual bool Remove(IFakeTable item)
        {
            return this.Cast().Remove(item);
        }

        protected virtual IFakeTable NewTable(string tableName)
        {
            return new FakeTable(this, tableName);
        }

        private HashSet<IFakeTable> Cast()
        {
            return this.Tables as HashSet<IFakeTable>;
        }
    }
}