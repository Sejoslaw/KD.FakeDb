using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace KD.FakeDb
{
    /// <summary>
    /// Fake Enumerator for getting next value in single Row.
    /// </summary>
    public class FakeRowEnumerator : IEnumerator<KeyValuePair<string, object>> // Key -> Column Name, Value -> value in that Column
    {
        public KeyValuePair<string, object> Current { get; private set; }

        private IFakeRow Row { get; }
        private int CurrentIndex { get; set; }

        object IEnumerator.Current
        {
            get
            {
                return this.Current;
            }
        }

        public FakeRowEnumerator(IFakeRow row)
        {
            this.Row = row;
            this.CurrentIndex = -1;
        }

        public void Dispose()
        {
            GC.Collect();
        }

        public bool MoveNext()
        {
            ++this.CurrentIndex;

            var columns = this.Row.Table.ColumnCollection.ToList();

            if (this.CurrentIndex >= columns.Count)
            {
                return false;
            }

            var column = columns[this.CurrentIndex];
            var columnValue = column[this.Row.Index];
            this.Current = new KeyValuePair<string, object>(column.Name, columnValue);

            return true;
        }

        public void Reset()
        {
            // Before the first element.
            this.CurrentIndex = -1;
        }
    }
}