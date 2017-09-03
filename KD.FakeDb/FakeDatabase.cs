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

        public string Name { get; set; }

        public IFakeTableCollection TableCollection { get; }

        public int Count
        {
            get
            {
                return this.TableCollection.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return this.TableCollection.IsReadOnly;
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

        public void Add(IFakeTable item)
        {
            this.TableCollection.Add(item);
        }

        public void Clear()
        {
            this.TableCollection.Clear();
        }

        public bool Contains(IFakeTable item)
        {
            return this.TableCollection.Contains(item);
        }

        public void CopyTo(IFakeTable[] array, int arrayIndex)
        {
            this.TableCollection.CopyTo(array, arrayIndex);
        }

        public bool Remove(IFakeTable item)
        {
            return this.TableCollection.Remove(item);
        }
    }
}