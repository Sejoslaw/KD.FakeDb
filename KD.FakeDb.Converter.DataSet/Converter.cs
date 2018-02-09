using System.Collections.Generic;
using System.Data;

namespace KD.FakeDb.Converter.DataSet
{
    /// <summary>
    /// Represents a converter used to convert "Fake" objects.
    /// </summary>
    public static partial class Converter
    {
        /// <summary>
        /// Converts current <see cref="IFakeDatabase"/> to <see cref="System.Data.DataSet"/>.
        /// </summary>
        public static System.Data.DataSet ToDataSet(this IFakeDatabase database)
        {
            // Create DataSet with IFakeDatabase name
            var dataSet = new System.Data.DataSet(database.Name);

            // For Each IFakeTable create new DataTable
            foreach (var fakeTable in database.Tables)
            {
                // Create new DataTable from IFakeTable
                var dataTable = new DataTable(fakeTable.Name);

                // For Each IFakeTable create new DataTable
                foreach (var fakeColumn in fakeTable.Columns)
                {
                    // Create and add new DataColumn to DataTable
                    var dataColumn = new DataColumn(fakeColumn.Name, fakeColumn.Type);
                    dataTable.Columns.Add(dataColumn);
                }

                // For Each IFakeRow add new DataRow to DataTable
                foreach (var fakeRow in fakeTable.Rows)
                {
                    // Create new DataRow
                    var dataRow = dataTable.NewRow();

                    // Add each record from IFakeRow to DataRow
                    foreach (var record in fakeRow)
                    {
                        // Fill DataRow with record values
                        dataRow[record.Key] = record.Value;
                    }

                    // Add DataRow to DataTable
                    dataTable.Rows.Add(dataRow);
                }

                // Add filled DataTable to DataSet
                dataSet.Tables.Add(dataTable);
            }

            // Return filled DataSet
            return dataSet;
        }

        /// <summary>
        /// Converts current <see cref="System.Data.DataSet"/> to given <see cref="IFakeDatabase"/>. 
        /// </summary>
        public static void ToFakeDatabase(this System.Data.DataSet dataSet, IFakeDatabase fakeDatabase)
        {
            // Set new name for Fake Database
            fakeDatabase.Name = dataSet.DataSetName;

            // Clear Fake Database and prepare it to fill with Tables from DataSet
            fakeDatabase.Clear();

            // Add each Table to Fake Database
            foreach (DataTable dataTable in dataSet.Tables)
            {
                // Create new Fake Table
                var fakeTable = fakeDatabase.AddTable(dataTable.TableName);

                // For each Column add new Fake Column
                foreach (DataColumn dataColumn in dataTable.Columns)
                {
                    // Create new Fake Column
                    var fakeColumn = fakeTable.AddColumn(dataColumn.ColumnName, dataColumn.DataType);

                    // Add Records to Fake Column
                    for (int i = 0; i < dataTable.Rows.Count; ++i)
                    {
                        DataRow dataRow = dataTable.Rows[i];

                        // Record value
                        var record = dataRow[dataColumn];

                        // Add record to Fake Column
                        fakeColumn.Add(new KeyValuePair<int, object>(i, record));
                    }
                }
            }
        }
    }
}