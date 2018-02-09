using System;
using System.Collections.Generic;
using System.Linq;

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
                return this.GetColumn(columnName);
            }
        }

        public virtual IFakeRow this[int rowIndex]
        {
            get
            {
                return this.GetRow(rowIndex);
            }
        }

        public string Name { get; set; }

        public IEnumerable<IFakeColumn> Columns { get; protected set; }
        public IEnumerable<IFakeRow> Rows { get; protected set; }
        public IFakeDatabase Database { get; }

        public FakeTable(IFakeDatabase fakeDatabase, string tableName)
        {
            this.Database = fakeDatabase;
            this.Name = tableName;
            this.Columns = new HashSet<IFakeColumn>();
            this.Rows = new HashSet<IFakeRow>();
        }

        public virtual void AddColumn(IFakeColumn column)
        {
            this.CastColumn().Add(column);
        }

        public virtual IFakeColumn AddColumn(string columnName, Type columnObjectType)
        {
            var cols = (from col in this.Columns
                        where col.Name.Equals(columnName)
                        select col).ToList();

            if (cols.Any())
            {
                throw new Exception($"Column with name \"{ columnName }\" already exists.");
            }

            var newColumn = this.NewColumn(columnName, columnObjectType);
            this.CastColumn().Add(newColumn);
            return newColumn;
        }

        public virtual IFakeRow AddRow(int rowIndex)
        {
            var rows = (from r in this.Rows
                        where r.Index == rowIndex
                        select r).ToList();

            if (rows.Count() > 0)
            {
                throw new Exception($"There is already a Row at index { rowIndex }");
            }

            IFakeRow row = this.NewRow(rowIndex);
            this.CastRow().Add(row);
            return row;
        }

        public virtual bool AddRow(IFakeRow row)
        {
            bool rowExists = this.Rows.Where(r => r.Index == row.Index).Any();

            if (rowExists)
            {
                throw new Exception($"There is already Row at index { row.Index }");
            }

            // If there isn't any row at specified index than add Row
            this.CastRow().Add(row);

            return true;
        }

        public virtual IFakeColumn GetColumn(string columnName)
        {
            var cols = (from col in this.Columns
                        where col.Name.Equals(columnName)
                        select col).ToList();

            if (cols.Count() > 1)
            {
                throw new Exception($"There is none or multiple Columns with name \"{ columnName }\"");
            }

            return cols.FirstOrDefault();
        }

        public virtual IFakeRow GetRow(int rowIndex)
        {
            var rows = (from r in this.Rows
                        where r.Index == rowIndex
                        select r).ToList();

            if (rows.Count() > 1)
            {
                throw new Exception($"There is none or multiple Rows at index \"{ rowIndex }\"");
            }

            return rows.FirstOrDefault();
        }

        public virtual void RemoveColumn(string columnName)
        {
            var cols = (from col in this.Columns
                        where col.Name.Equals(columnName)
                        select col);

            if (cols.Count() > 1)
            {
                throw new Exception(string.Format("There is none or multiple Columns with name \"{0}\"", columnName));
            }

            this.CastColumn().Remove(cols.FirstOrDefault());
        }

        public virtual void RemoveRow(int rowIndex)
        {
            IFakeRow row = this.GetRow(rowIndex);
            this.CastRow().Remove(row);
        }

        protected virtual IFakeColumn NewColumn(string columnName, Type columnObjectType)
        {
            return new FakeColumn(this, columnName, columnObjectType);
        }

        protected virtual IFakeRow NewRow(int rowIndex)
        {
            return new FakeRow(this, rowIndex);
        }

        private HashSet<IFakeColumn> CastColumn()
        {
            return this.Columns as HashSet<IFakeColumn>;
        }

        private HashSet<IFakeRow> CastRow()
        {
            return this.Rows as HashSet<IFakeRow>;
        }
    }
}