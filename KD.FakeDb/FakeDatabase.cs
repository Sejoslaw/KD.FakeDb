using System.Collections;
using System.Collections.Generic;

namespace KD.FakeDb
{
    /// <summary>
    /// Default implementaion of <see cref="IFakeDatabase"/>.
    /// </summary>
    public class FakeDatabase : IFakeDatabase
    {
        public IFakeTable this[string tableName]
        {
            get
            {
                return this.TableCollection[tableName];
            }
        }

        public IFakeTableCollection TableCollection { get; }

        public int Count
        {
            get
            {
                return this.TableCollection.Count;
            }
        }

        public FakeDatabase()
        {
            this.TableCollection = new FakeTableCollection(this);
        }

        public IFakeTable AddTable(string tableName)
        {
            return this.TableCollection.AddTable(tableName);
        }

        public IFakeTable GetTable(string tableName)
        {
            return this.TableCollection.GetTable(tableName);
        }

        public void RemoveTable(string tableName)
        {
            this.TableCollection.RemoveTable(tableName);
        }

        public IEnumerator<IFakeTable> GetEnumerator()
        {
            return this.TableCollection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}