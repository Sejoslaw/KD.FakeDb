using System;

namespace KD.FakeDb
{
    /// <summary>
    /// Default implementation of <see cref="IFakeTable"/>.
    /// </summary>
    public class FakeTable : IFakeTable
    {
        public virtual IFakeColumn this[string columnName]
        {
            get
            {
                return this.ColumnCollection[columnName];
            }
        }

        public virtual IFakeRow this[int rowIndex]
        {
            get
            {
                return this.RowCollection[rowIndex];
            }
        }

        public string Name { get; set; }

        public IFakeColumnCollection ColumnCollection { get; protected set; }
        public IFakeRowCollection RowCollection { get; protected set; }
        public IFakeDatabase Database { get; }

        public FakeTable(IFakeDatabase fakeDatabase, string tableName)
        {
            this.Database = fakeDatabase;
            this.Name = tableName;
            this.ColumnCollection = new FakeColumnCollection(this);
            this.RowCollection = new FakeRowCollection(this);
        }

        public virtual void AddColumn(IFakeColumn column)
        {
            this.ColumnCollection.Add(column);
        }

        public virtual IFakeColumn AddColumn(string columnName, Type columnObjectType)
        {
            return this.ColumnCollection.AddColumn(columnName, columnObjectType);
        }

        public virtual bool AddRow(IFakeRow row)
        {
            return this.RowCollection.AddRow(row);
        }

        public virtual IFakeColumn GetColumn(string columnName)
        {
            return this.ColumnCollection.GetColumn(columnName);
        }

        public virtual IFakeRow GetRow(int rowIndex)
        {
            return this.RowCollection.GetRow(rowIndex);
        }

        public virtual void RemoveColumn(string columnName)
        {
            this.ColumnCollection.RemoveColumn(columnName);
        }

        public virtual void RemoveRow(int rowIndex)
        {
            this.RowCollection.RemoveRow(rowIndex);
        }
    }
}