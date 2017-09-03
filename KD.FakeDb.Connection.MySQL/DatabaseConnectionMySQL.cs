using System;
using MySql.Data.MySqlClient;

namespace KD.FakeDb.Connection.MySQL
{
    /// <summary>
    /// MySQL implementation of <see cref="DatabaseConnection{TDbConnection, TDbDataReader}"/>.
    /// </summary>
    public class DatabaseConnectionMySQL : DatabaseConnection<MySqlConnection, MySqlDataReader>
    {
        /// <summary>
        /// Name of the Schema Database.
        /// </summary>
        private readonly string SCHEMA_TABLE_NAME = "INFORMATION_SCHEMA";
        /// <summary>
        /// Name of the Table which contains tables definitions.
        /// </summary>
        private readonly string SCHEMA_TABLES = "TABLES";
        /// <summary>
        /// Name of the Table which contains columns definitions.
        /// </summary>
        private readonly string SCHEMA_COLUMNS = "COLUMNS";

        public override MySqlDataReader GetAllTableNames()
        {
            return PrepareCommand(string.Format("SELECT * FROM {0}.{1} WHERE {2} = '{3}'",
                SCHEMA_TABLE_NAME, SCHEMA_TABLES,
                GetColumnNameWithDatabaseSchema(), this.Database.Name));
        }

        public override string GetColumnNameWithColumnDataType()
        {
            return "DATA_TYPE";
        }

        public override string GetColumnNameWithTableColumnNames()
        {
            return "COLUMN_NAME";
        }

        public override string GetColumnNameWithTableNames()
        {
            return "TABLE_NAME";
        }

        public override string GetColumnNameWithDatabaseSchema()
        {
            return "TABLE_SCHEMA";
        }

        public override MySqlDataReader GetColumnsForTable(string tableName)
        {
            return PrepareCommand(string.Format("SELECT * FROM {0} WHERE {1} = '{2}' AND {3} = '{4}'",
                SCHEMA_COLUMNS,
                GetColumnNameWithTableNames(), tableName,
                GetColumnNameWithDatabaseSchema(), this.Database.Name));
        }

        public override Type GetColumnValueType(string columnDataTypeName)
        {
            return typeof(object);

            // TODO: Add actual mapping for MySQL types. ->  case "MySQL Type": retutn typeof(C# Type);
            //switch (columnDataTypeName)
            //{
            //    case "int": return typeof(int);
            //    case "varchar": return typeof(string);
            //    default: throw new Exception(string.Format("Unknown Column Data Type {0}", columnDataTypeName));
            //}
        }

        public override MySqlDataReader GetTable(string tableName)
        {
            return PrepareCommand(string.Format("SELECT * FROM {0}.{1}", this.Database.Name, tableName));
        }

        private MySqlDataReader PrepareCommand(string command)
        {
            var mySqlCommand = new MySqlCommand(command, this.Connection);
            var reader = mySqlCommand.ExecuteReader();
            return reader;
        }
    }
}