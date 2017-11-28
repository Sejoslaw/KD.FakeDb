using System.Collections;
using System.Collections.Generic;

namespace KD.FakeDb
{
    /// <summary>
    /// Default implementaion of <see cref="IFakeDatabase"/>.
    /// </summary>
    public class FakeDatabase : IFakeDatabase
    {
        public virtual IFakeTable this[string tableName]
        {
            get
            {
                return this.TableCollection[tableName];
            }
        }

        public string Name { get; set; }
        public IFakeTableCollection TableCollection { get; protected set; }

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

        public virtual IFakeTable AddTable(string tableName)
        {
            return this.TableCollection.AddTable(tableName);
        }

        public virtual IFakeTable GetTable(string tableName)
        {
            return this.TableCollection.GetTable(tableName);
        }

        public virtual void RemoveTable(string tableName)
        {
            this.TableCollection.RemoveTable(tableName);
        }

        public virtual IEnumerator<IFakeTable> GetEnumerator()
        {
            return this.TableCollection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public virtual void Add(IFakeTable item)
        {
            this.TableCollection.Add(item);
        }

        public virtual void Clear()
        {
            this.TableCollection.Clear();
        }

        public virtual bool Contains(IFakeTable item)
        {
            return this.TableCollection.Contains(item);
        }

        public virtual void CopyTo(IFakeTable[] array, int arrayIndex)
        {
            this.TableCollection.CopyTo(array, arrayIndex);
        }

        public virtual bool Remove(IFakeTable item)
        {
            return this.TableCollection.Remove(item);
        }
    }
}