using System;
using System.Data.Common;

namespace KD.FakeDb.Connection
{
    /// <summary>
    /// Abstract type of Connection which has pre-filled SQL statements.
    /// </summary>
    /// <typeparam name="TDbConnection"> Type of <see cref="DbConnection"/>. </typeparam>
    /// <typeparam name="TDbDataReader"> Type of <see cref="DbDataReader"/>. </typeparam>
    public abstract class PreFilledDatabaseConnection<TDbConnection, TDbDataReader> : DatabaseConnection<TDbConnection, TDbDataReader>
        where TDbConnection : DbConnection
        where TDbDataReader : DbDataReader
    {
        /// <summary>
        /// Returns <see cref="TDbDataReader"/> from given command.
        /// 
        /// What here should happen:
        /// 1. Prepare DbCommand
        /// 2. ExecuteReader on DbCommand and return result
        /// </summary>
        public abstract TDbDataReader PrepareCommand(string command);

        /// <summary>
        /// Returns the name of an Information Schema Table.
        /// </summary>
        public virtual string GetTableInformationSchema()
        {
            return "INFORMATION_SCHEMA";
        }

        /// <summary>
        /// Returns the name of a Schema Tables Table.
        /// </summary>
        public virtual string GetTableSchemaTables()
        {
            return "TABLES";
        }

        /// <summary>
        /// Returns the name of a Schema Columns Table.
        /// </summary>
        public virtual string GetTableSchemaColumns()
        {
            return "COLUMNS";
        }

        public override TDbDataReader GetAllTableNames()
        {
            return PrepareCommand(string.Format("SELECT * FROM {0}.{1} WHERE {2} = '{3}'",
                GetTableInformationSchema(), GetTableSchemaTables(),
                GetColumnNameWithDatabaseSchema(), this.Database.Name));
        }

        public override TDbDataReader GetColumnsForTable(string tableName)
        {
            return PrepareCommand(string.Format("SELECT * FROM {0} WHERE {1} = '{2}' AND {3} = '{4}'",
                GetTableSchemaColumns(),
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

        public override TDbDataReader GetTable(string tableName)
        {
            return PrepareCommand(string.Format("SELECT * FROM {0}.{1}", this.Database.Name, tableName));
        }
    }
}