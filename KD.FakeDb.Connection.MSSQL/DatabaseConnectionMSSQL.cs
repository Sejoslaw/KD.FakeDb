using System.Data.SqlClient;

namespace KD.FakeDb.Connection.MSSQL
{
    /// <summary>
    /// Microsoft SQL (MSSQL) implementation of <see cref="DatabaseConnection{TDbConnection, TDbDataReader}"/>
    /// </summary>
    public class DatabaseConnectionMSSQL : PreFilledDatabaseConnection<SqlConnection, SqlDataReader>
    {
        public override string GetTableSchemaColumns()
        {
            return GetTableInformationSchema() + "." + base.GetTableSchemaColumns();
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
            return "TABLE_CATALOG";
        }

        public override SqlDataReader GetTable(string tableName)
        {
            return PrepareCommand(string.Format("SELECT * FROM {0}.{1}", "dbo", tableName));
        }

        public override SqlDataReader PrepareCommand(string command)
        {
            var mySqlCommand = new SqlCommand(command, this.Connection);
            var reader = mySqlCommand.ExecuteReader();
            return reader;
        }
    }
}