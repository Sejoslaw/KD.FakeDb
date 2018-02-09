using System;
using System.Collections.Generic;
using System.Data.Common;

namespace KD.FakeDb.Connection
{
    /// <summary>
    /// Connection which is used for mapping Real Database to <see cref="IFakeDatabase"/>.
    /// </summary>
    /// <typeparam name="TDbConnection"> Type of <see cref="DbConnection"/>. </typeparam>
    /// <typeparam name="TDbDataReader"> Type of <see cref="DbDataReader"/>. </typeparam>
    public abstract class DatabaseConnection<TDbConnection, TDbDataReader>
        where TDbConnection : DbConnection
        where TDbDataReader : DbDataReader
    {
        /// <summary>
        /// Delegate which determine what should happen after the record ahs been read successfully.
        /// </summary>
        /// <param name="fakeDatabase"> <see cref="IFakeDatabase"/> to which record was read. </param>
        /// <param name="fakeTable"> <see cref="IFakeTable"/> to which record was read. </param>
        /// <param name="fakeColumn"> <see cref="IFakeColumn"/> to which record was read. </param>
        /// <param name="recordIndex"> Index of currently read record. </param>
        /// <param name="tableColumnValue"> Value of currently read record. </param>
        public delegate void RecordReadDelegate(IFakeDatabase fakeDatabase, IFakeTable fakeTable, IFakeColumn fakeColumn, int recordIndex, object tableColumnValue);

        /// <summary>
        /// Database to which the information will be written.
        /// By default it should be empty but NOT null.
        /// </summary>
        public IFakeDatabase Database { get; set; }
        /// <summary>
        /// Connection used to connect to Database.
        /// </summary>
        public TDbConnection Connection { get; set; }
        /// <summary>
        /// Delegate which determine what should happen after the record ahs been read successfully.
        /// </summary>
        public RecordReadDelegate OnRecordRead { get; set; }

        /// <summary>
        /// Default Constructor.
        /// </summary>
        public DatabaseConnection()
        {
            // Set default body for OnRecordRead delegate.
            this.OnRecordRead += (fakeDatabase, fakeTable, fakeColumn, recordIndex, tableColumnValue) =>
            {
                // TODO: Add Check for right Type
                //if (tableColumnValue.GetType() != fakeColumn.Type)
                //{
                //    throw new Exception(string.Format("Readded Column value type ({0}) is different than the Fake Column can take ({1}).", tableColumnValue.GetType(), fakeColumn.Type));
                //}

                // Be default just add the record to column.
                fakeColumn.Add(new KeyValuePair<int, object>(recordIndex, tableColumnValue));
            };
        }

        /// <summary>
        /// Returns the Name of a Column which holds Table Names.
        /// By default it should be something like "TABLE_NAME".
        /// </summary>
        public abstract string GetColumnNameWithTableNames();

        /// <summary>
        /// Returns the Name of a Column which holds Names of Columns for specified Table.
        /// By default it should be something like "COLUMN_NAME".
        /// </summary>
        public abstract string GetColumnNameWithTableColumnNames();

        /// <summary>
        /// Returns the name of a Column which holds Name of a data type of Column.
        /// By default it should be something like "DATA_TYPE".
        /// DATA_TYPE - holds name of a data type inside Column. Ex: char, varchar, int, etc.
        /// COLUMN_TYPE - holds full name of a data type inside Column. Ex: char(36), varchar(50), int, etc.
        /// </summary>
        public abstract string GetColumnNameWithColumnDataType();

        /// <summary>
        /// Returns name of Column which contains Database Name.
        /// By default it should be something like "TABLE_SCHEMA".
        /// </summary>
        public abstract string GetColumnNameWithDatabaseSchema();

        /// <summary>
        /// Returns C# <see cref="Type"/> from given Database data type. Ex: char, varchar, int, etc.
        /// </summary>
        public abstract Type GetColumnValueType(string columnDataTypeName);

        /// <summary>
        /// Returns all Table names from given Database name using current <see cref="DbConnection"/>.
        /// The standard is that all Table names will be in column name called "TABLE_NAME".
        /// Here should mainly be called SQL statement that returns all Table names.
        /// </summary>
        public abstract TDbDataReader GetAllTableNames();

        /// <summary>
        /// Returns <see cref="DbDataReader"/> from which data for each Column will be mapped to <see cref="IFakeTable"/>.
        /// </summary>
        public abstract TDbDataReader GetTable(string tableName);

        /// <summary>
        /// Returns Columns in order for given Table.
        /// </summary>
        public abstract TDbDataReader GetColumnsForTable(string tableName);

        /// <summary>
        /// Converts Database by it's name (if exists) and set local variable <see cref="Database"/> as result.
        /// </summary>
        /// <param name="databaseName"></param>
        public virtual void ToFake(string databaseName)
        {
            // Check if Fake Database is set
            if (this.Database == null)
            {
                throw new Exception("Fake Database should be empty but not null.");
            }

            // Set Database Name
            this.Database.Name = databaseName;

            // Read all Table Names for wanted Database
            var tableNames = GetAllTableNamesWrapped(); // Table Names, ex: Accounts, Countries

            foreach (var tableName in tableNames)
            {
                // Create new Fake Table
                var fakeTable = this.Database.AddTable(tableName);

                // Read Column Names for current Table
                var columns = GetColumnsForTableWrapped(tableName); // Column Names, ex: Id, FirstName, LastName

                foreach (var column in columns)
                {
                    // Create new FakeColumn
                    fakeTable.AddColumn(column.Key, column.Value);
                }

                // Open Connection for reading Table.
                this.Connection.Open();

                // Read Full Table from Database
                var readerTable = GetTable(tableName);

                // Record index of each readded Record
                int recordIndex = 0;
                // While Table contains Records
                while (readerTable.Read())
                {
                    // For each Fake Column check name and try to read value of current row and add it to Fake Column
                    foreach (var fakeColumn in fakeTable.Columns)
                    {
                        int tableColumnOrdinal = readerTable.GetOrdinal(fakeColumn.Name);
                        // Found Value
                        var tableColumnValue = readerTable.GetValue(tableColumnOrdinal);
                        // Use delegate to add value to database
                        OnRecordRead(Database, fakeTable, fakeColumn, recordIndex, tableColumnValue);

                        ++recordIndex;
                    }
                }

                // Close after reading Table.
                this.Connection.Close();
            }

            // Close any possible open Connection.
            this.Connection.Close();
        }

        /// <summary>
        /// Wrapped version of GetAllTableNames() for internal use.
        /// </summary>
        private List<string> GetAllTableNamesWrapped()
        {
            List<string> names = new List<string>();

            this.Connection.Open();

            var reader = this.GetAllTableNames();
            while (reader.Read())
            {
                // Read Table Name
                names.Add(reader[GetColumnNameWithTableNames()].ToString());
            }

            this.Connection.Close();

            return names;
        }

        /// <summary>
        /// Wrapped version of GetColumnsForTable(tableName) for internal use.
        /// </summary>
        private Dictionary<string, Type> GetColumnsForTableWrapped(string tableName)
        {
            Dictionary<string, Type> columns = new Dictionary<string, Type>();

            this.Connection.Open();

            var reader = this.GetColumnsForTable(tableName);
            while (reader.Read())
            {
                // Read Column Name
                var columnName = reader[GetColumnNameWithTableColumnNames()].ToString();

                // Read Data Type which should be inside this column
                var columnDataTypeName = reader[GetColumnNameWithColumnDataType()].ToString();
                Type columnDataType = GetColumnValueType(columnDataTypeName);

                columns.Add(columnName, columnDataType);
            }

            this.Connection.Close();

            return columns;
        }
    }
}