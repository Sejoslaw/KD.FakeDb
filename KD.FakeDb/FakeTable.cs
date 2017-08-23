using System;

namespace KD.FakeDb
{
    /// <summary>
    /// Default implementation of <see cref="IFakeTable"/>.
    /// </summary>
    public class FakeTable : IFakeTable
    {
        public IFakeColumn this[string columnName]
        {
            get
            {
                return this.ColumnCollection[columnName];
            }
        }

        public IFakeRow this[int rowIndex]
        {
            get
            {
                return this.RowCollection[rowIndex];
            }
        }

        public string Name { get; set; }

        public IFakeColumnCollection ColumnCollection { get; }

        public IFakeRowCollection RowCollection { get; }

        public IFakeDatabase Database { get; }

        public FakeTable(IFakeDatabase fakeDatabase, string tableName)
        {
            this.Database = fakeDatabase;
            this.Name = tableName;
            this.ColumnCollection = new FakeColumnCollection(this);
            this.RowCollection = new FakeRowCollection(this);
        }

        public IFakeColumn AddColumn(string columnName, Type columnObjectType)
        {
            return this.ColumnCollection.AddColumn(columnName, columnObjectType);
        }

        public bool AddRow(IFakeRow row)
        {
            return this.RowCollection.AddRow(row);
        }

        public IFakeColumn GetColumn(string columnName)
        {
            return this.ColumnCollection.GetColumn(columnName);
        }

        public IFakeRow GetRow(int rowIndex)
        {
            return this.RowCollection.GetRow(rowIndex);
        }

        public void RemoveColumn(string columnName)
        {
            this.ColumnCollection.RemoveColumn(columnName);
        }

        public void RemoveRow(int rowIndex)
        {
            this.RowCollection.RemoveRow(rowIndex);
        }
    }
}