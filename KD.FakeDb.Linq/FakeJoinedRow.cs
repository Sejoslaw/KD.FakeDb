using System.Collections;
using System.Collections.Generic;

namespace KD.FakeDb.Linq
{
    /// <summary>
    /// Default implementation of <see cref="IFakeJoinedRow"/>.
    /// </summary>
    public class FakeJoinedRow : IFakeJoinedRow
    {
        public object this[string columnName]
        {
            get
            {
                return this.Values[columnName];
            }
        }

        public IDictionary<string, object> Values { get; private set; }

        public FakeJoinedRow()
        {
            this.Values = new Dictionary<string, object>();
        }

        public FakeJoinedRow(IFakeRow row)
            : this()
        {
            JoinRow(row);
        }

        public bool CanJoinRow(IFakeRow row, string columnName)
        {
            return this.CanJoinRow(row.ToJoinedRow(), columnName);
        }

        public bool CanJoinRow(IFakeJoinedRow row, string columnName)
        {
            try
            {
                // Try to get value and if value does not exist , exception will be thrown.
                // It also means that given Row doesn't contains column with specified name.
                var x = row.Values[columnName];

                if (this.Values.ContainsKey(columnName))
                {
                    return true;
                }
                return false;
            }
            catch (KeyNotFoundException)
            {
                throw new KeyNotFoundException(string.Format("Give column ({0}) does not exist in given Row.", columnName));
            }
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return this.Values.GetEnumerator();
        }

        public void JoinRow(IFakeRow row)
        {
            row.ForEach(element =>
            {
                if (!this.Values.ContainsKey(element.Key))
                {
                    this.Values.Add(element.Key, element.Value);
                }
            });
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public void JoinRow(IFakeJoinedRow row)
        {
            row.ForEach(element =>
            {
                if (!this.Values.ContainsKey(element.Key))
                {
                    this.Values.Add(element.Key, element.Value);
                }
            });
        }

        public IFakeJoinedRow JoinToNew(IFakeJoinedRow row)
        {
            IFakeJoinedRow newRow = new FakeJoinedRow();
            newRow.JoinRow(this);
            newRow.JoinRow(row);
            return newRow;
        }
    }
}