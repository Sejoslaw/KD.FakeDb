using MySql.Data.MySqlClient;

namespace KD.FakeDb.Connection.MySQL
{
    /// <summary>
    /// MySQL implementation of <see cref="PreFilledDatabaseConnection{TDbConnection, TDbDataReader}"/>.
    /// </summary>
    public class DatabaseConnectionMySQL : PreFilledDatabaseConnection<MySqlConnection, MySqlDataReader>
    {
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

        public override MySqlDataReader PrepareCommand(string command)
        {
            var mySqlCommand = new MySqlCommand(command, this.Connection);
            var reader = mySqlCommand.ExecuteReader();
            return reader;
        }
    }
}