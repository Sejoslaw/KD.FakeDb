using System.Collections.Generic;
using System.IO;

namespace KD.FakeDb.Export.Files.CSV
{
    public class FakeDbExporterCSVConfiguration : FakeDbExporterAbstractFileConfiguration
    {
        /// <summary>
        /// It will export given <see cref="IFakeDatabase"/> using given <see cref="FileStream"/>.
        /// It will flush and close the given <see cref="FileStream"/>.
        /// </summary>
        protected override void ExportToFile(FileStream stream, IFakeDatabase database)
        {
            List<string> lines = new List<string>();

            // Add Database Name
            lines.Add(database.Name);

            // Add Each Table to String Builder
            foreach (var table in database.TableCollection)
            {
                lines.Add("");
                // Add Table Name
                lines.Add(table.Name);

                // Add Column Names from Table
                string columnNames = "";
                foreach (var column in table.ColumnCollection)
                {
                    columnNames += column.Name + ",";
                }
                lines.Add(columnNames);

                // Add each Row from current Table
                foreach (var row in table.RowCollection)
                {
                    string rowValues = "";
                    foreach (var record in row)
                    {
                        rowValues += record.Value + ",";
                    }
                    lines.Add(rowValues);
                }
            }

            // Store path to File.
            string path = stream.Name;

            // Flush and Close stream
            stream.Flush();
            stream.Close();

            // Add Everything from StringBuilder to File
            File.AppendAllLines(path, lines);
        }
    }
}