using System;
using System.Collections;
using System.Collections.Generic;

namespace KD.FakeDb
{
    /// <summary>
    /// Default implementation of <see cref="IFakeColumn"/>.
    /// </summary>
    public class FakeColumn : IFakeColumn
    {
        /// <summary>
        /// Represents a collection of objects in single <see cref="IFakeColumn"/>.
        /// </summary>
        private List<object> objects = new List<object>();

        public object this[int index]
        {
            get
            {
                return this.objects[index];
            }
            set
            {
                if (value.GetType() != this.Type)
                {
                    throw new Exception(string.Format("Value with wrong type was tried to be added to this Column (Value Type = {0}). This Column accepts values with type = {1}", value.GetType(), this.Type));
                }

                this.objects[index] = value;
            }
        }

        public string Name { get; set; }

        public Type Type { get; }

        public int Count
        {
            get
            {
                return this.objects.Count;
            }
        }

        public IFakeTable Table { get; }

        public FakeColumn(IFakeTable table, string columnName, Type columnObjectType)
        {
            this.Table = table;
            this.Name = columnName;
            this.Type = columnObjectType;
        }

        public IEnumerator<object> GetEnumerator()
        {
            return this.objects.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}