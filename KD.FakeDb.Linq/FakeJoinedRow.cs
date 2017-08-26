using System.Collections;
using System.Collections.Generic;

namespace KD.FakeDb.Linq
{
    /// <summary>
    /// Default implementation of <see cref="IFakeJoinedRow"/>.
    /// </summary>
    public class FakeJoinedRow : IFakeJoinedRow
    {
        public IDictionary<string, object> Values { get; private set; }

        public FakeJoinedRow(IFakeRow row)
        {
            this.Values = new Dictionary<string, object>();

            this.JoinRow(row);
        }

        public bool CanJoinRow(IFakeRow row, string columnName)
        {
            try
            {
                // Try to get value and if value does not exist , exception will be thrown.
                // It also means that given Row doesn't contains column with specified name.
                var x = row[columnName];

                if (this.Values.ContainsKey(columnName))
                {
                    return true;
                }
                return false;
            }
            catch (KeyNotFoundException ex)
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
    }
}