using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace KD.FakeDb
{
    /// <summary>
    /// Default implementation of <see cref="IFakeRow"/>.
    /// </summary>
    public class FakeRow : IFakeRow
    {
        public virtual object this[string columnName]
        {
            get
            {
                IFakeColumn col = this.Table.GetColumn(columnName);

                if (col == null)
                {
                    return null;
                }

                object value = col[this.Index];
                return value;
            }
            set
            {
                IFakeColumn col = this.Table.GetColumn(columnName);

                if (col != null)
                {
                    col[this.Index] = value;
                }
            }
        }

        public virtual object this[int columnIndex]
        {
            get
            {
                IFakeColumn col = this.Table.Columns.ElementAt(columnIndex);
                object value = col[this.Index];
                return value;
            }
            set
            {
                IFakeColumn col = this.Table.Columns.ElementAt(columnIndex);
                col[this.Index] = value;
            }
        }

        public int Index { get; }

        public int Count
        {
            get
            {
                // Number of elements in one row is a number of columns.
                return this.Table.Columns.Count();
            }
        }

        public IFakeTable Table { get; }

        public FakeRow(IFakeTable fakeTable, int index)
        {
            this.Table = fakeTable;
            this.Index = index;
        }

        public virtual IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            foreach (IFakeColumn column in this.Table.Columns)
            {
                object value = column[this.Index];
                yield return new KeyValuePair<string, object>(column.Name, value);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}