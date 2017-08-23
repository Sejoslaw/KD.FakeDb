using System;
using System.Collections.Generic;
using System.Linq;

namespace KD.FakeDb
{
    /// <summary>
    /// Fake Enumerator for getting next value in single Row.
    /// </summary>
    public class FakeRowEnumerator : IEnumerator<object>
    {
        public object Current { get; private set; }

        private IFakeRow Row { get; }
        private int CurrentIndex { get; set; }

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

            this.Current = columns[this.CurrentIndex][this.Row.Index];

            return true;
        }

        public void Reset()
        {
            // Before the first element.
            this.CurrentIndex = -1;
        }
    }
}