using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace KD.FakeDb
{
    /// <summary>
    /// Default implementation of <see cref="IFakeColumnCollection"/>.
    /// </summary>
    public class FakeColumnCollection :  IFakeColumnCollection
    {
        /// <summary>
        /// For internal <see cref="IFakeColumn"/>'s storing.
        /// </summary>
        protected IList<IFakeColumn> columns = new List<IFakeColumn>();

        public virtual IFakeColumn this[string columnName]
        {
            get
            {
                return GetColumn(columnName);
            }
        }

        public virtual IFakeColumn this[int columnIndex]
        {
            get
            {
                return this.columns[columnIndex];
            }
        }

        public int Count
        {
            get
            {
                return columns.Count;
            }
        }

        public IFakeTable Table { get; }

        public bool IsReadOnly => throw new NotImplementedException();

        public FakeColumnCollection(IFakeTable fakeTable)
        {
            this.Table = fakeTable;
        }

        public virtual IFakeColumn AddColumn(string columnName, Type columnObjectType)
        {
            var cols = (from col in this.columns
                        where col.Name.Equals(columnName)
                        select col);

            if (cols.Any())
            {
                throw new Exception(string.Format("Column with name \"{0}\" already exists.", columnName));
            }

            var newColumn = new FakeColumn(this.Table, columnName, columnObjectType);
            this.columns.Add(newColumn);
            return newColumn;
        }

        public virtual IFakeColumn GetColumn(string columnName)
        {
            var cols = (from col in this.columns
                        where col.Name.Equals(columnName)
                        select col);

            if (cols.Count() != 1)
            {
                throw new Exception(string.Format("There is none or multiple Columns with name \"{0}\"", columnName));
            }

            return cols.First();
        }

        public virtual void RemoveColumn(string columnName)
        {
            var cols = (from col in this.columns
                        where col.Name.Equals(columnName)
                        select col);

            if (cols.Count() != 1)
            {
                throw new Exception(string.Format("There is none or multiple Columns with name \"{0}\"", columnName));
            }

            this.columns.Remove(cols.First());
        }

        public virtual IEnumerator<IFakeColumn> GetEnumerator()
        {
            return this.columns.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public virtual void Add(IFakeColumn item)
        {
            this.columns.Add(item);
        }

        public virtual void Clear()
        {
            this.columns.Clear();
        }

        public virtual bool Contains(IFakeColumn item)
        {
            return this.columns.Contains(item);
        }

        public virtual void CopyTo(IFakeColumn[] array, int arrayIndex)
        {
            this.columns.CopyTo(array, arrayIndex);
        }

        public virtual bool Remove(IFakeColumn item)
        {
            return this.columns.Remove(item);
        }
    }
}