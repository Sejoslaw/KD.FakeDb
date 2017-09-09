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
            foreach (var fakeTable in database.TableCollection)
            {
                // Create new DataTable from IFakeTable
                var dataTable = new DataTable(fakeTable.Name);

                // For Each IFakeTable create new DataTable
                foreach (var fakeColumn in fakeTable.ColumnCollection)
                {
                    // Create and add new DataColumn to DataTable
                    var dataColumn = new DataColumn(fakeColumn.Name, fakeColumn.Type);
                    dataTable.Columns.Add(dataColumn);
                }

                // For Each IFakeRow add new DataRow to DataTable
                foreach (var fakeRow in fakeTable.RowCollection)
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
    }
}