﻿using System.Collections;
using System.Collections.Generic;

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
                return this.Table.ColumnCollection[columnName][this.Index];
            }
            set
            {
                this.Table.ColumnCollection[columnName][this.Index] = value;
            }
        }

        public virtual object this[int columnIndex]
        {
            get
            {
                return this.Table.ColumnCollection[columnIndex][this.Index];
            }
            set
            {
                this.Table.ColumnCollection[columnIndex][this.Index] = value;
            }
        }

        public int Index { get; }

        public int Count
        {
            get
            {
                // Number of elements in one row is a number of columns.
                return this.Table.ColumnCollection.Count;
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
            return new FakeRowEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}