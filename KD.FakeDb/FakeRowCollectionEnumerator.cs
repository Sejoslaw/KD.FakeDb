using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace KD.FakeDb
{
    /// <summary>
    /// Fake Enumerator for getting next Row.
    /// </summary>
    public class FakeRowCollectionEnumerator : IEnumerator<IFakeRow>
    {
        public IFakeRow Current { get; private set; }

        object IEnumerator.Current { get; }

        private IFakeRowCollection FakeRowCollection { get; }
        private int CurrentIndex { get; set; }

        public FakeRowCollectionEnumerator(IFakeRowCollection fakeRowCollection)
        {
            this.FakeRowCollection = fakeRowCollection;
            this.CurrentIndex = -1;
        }

        public void Dispose()
        {
            GC.Collect();
        }

        public bool MoveNext()
        {
            ++this.CurrentIndex;

            var columns = this.FakeRowCollection.Table.ColumnCollection.ToList();
            var columnElements = columns[0].ToList().Count;

            if (this.CurrentIndex >= columnElements)
            {
                return false;
            }

            this.Current = this.FakeRowCollection.GetRow(this.CurrentIndex);

            return true;
        }

        public void Reset()
        {
            this.CurrentIndex = -1;
        }
    }
}