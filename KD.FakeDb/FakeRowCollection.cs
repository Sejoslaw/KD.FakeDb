using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace KD.FakeDb
{
    /// <summary>
    /// Default implementation of <see cref="IFakeRowCollection"/>.
    /// </summary>
    public class FakeRowCollection : IFakeRowCollection
    {
        public IFakeRow this[int rowIndex]
        {
            get
            {
                return GetRow(rowIndex);
            }
        }

        public IFakeTable Table { get; }

        public int Count
        {
            get
            {
                return this.Table.ColumnCollection.ToList()[0].Count;
            }
        }

        public FakeRowCollection(IFakeTable fakeTable)
        {
            this.Table = fakeTable;
        }

        public bool AddRow(IFakeRow row)
        {
            var columnList = this.Table.ColumnCollection.ToList();
            var index = row.Index;
            bool rowExists = false;

            /// Check if there is at least one value on new row index
            columnList.ForEach(column =>
            {
                rowExists |= column[index] != null;
            });

            if (rowExists)
            {
                throw new Exception(string.Format("There is already Row at index {0}", index));
            }

            // If there isn't any row at specified index than add Row
            columnList.ForEach(column =>
            {
                column[index] = row[column.Name];
            });

            return true;
        }

        public IEnumerator<IFakeRow> GetEnumerator()
        {
            return new FakeRowCollectionEnumerator(this);
        }

        public IFakeRow GetRow(int rowIndex)
        {
            return new FakeRow(this.Table, rowIndex);
        }

        public void RemoveRow(int rowIndex)
        {
            this.Table.ColumnCollection.ToList().ForEach(column => column[rowIndex] = null);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}