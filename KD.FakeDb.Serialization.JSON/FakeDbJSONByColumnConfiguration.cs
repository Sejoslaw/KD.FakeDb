using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KD.FakeDb.Serialization.JSON
{
    /// <summary>
    /// Configuration which will save each <see cref="IFakeDatabase"/> by it's columns.
    /// </summary>
    public class FakeDbJSONByColumnConfiguration : IFakeDbJSONConfiguration
    {
        public void ReadDatabase(JsonReader reader, IFakeDatabase database)
        {
            // Parse JSON
            var databaseJSON = JObject.Load(reader); // Load JSON Database object
            var databaseType = GetTypeFromProperty(databaseJSON); // Database Type

            // Create Database
            database = SerializerUtils.TryToBuildObject<IFakeDatabase>(databaseType, null);

            if (database == null)
            {
                throw new Exception("Database is null, after setting.");
            }

            // Set Database Name
            database.Name = databaseJSON[FakeDbConstants.PropertyName].Value<string>();

            // For Each Table in file
            foreach (var tableJSON in databaseJSON.Property(FakeDbConstants.LabelTable).Value.AsJEnumerable())
            {
                // Build Table
                var tableName = tableJSON[FakeDbConstants.PropertyName].Value<string>();
                var tableType = Type.GetType(tableJSON[FakeDbConstants.PropertyClass].Value<string>()); // Table Type
                var table = SerializerUtils.TryToBuildObject<IFakeTable>(tableType, new object[] { database, tableName });

                // Force change the Table Name
                table.Name = tableName;

                // For each Column in Table
                foreach (var columnJSON in tableJSON[FakeDbConstants.LabelColumn])
                {
                    // Build Column
                    var columnName = columnJSON[FakeDbConstants.PropertyName].Value<string>();
                    var columnType = Type.GetType(columnJSON[FakeDbConstants.PropertyClass].Value<string>()); // Column Type

                    var columnRecordType = Type.GetType(columnJSON[FakeDbConstants.PropertyColumnRecordType].Value<string>());

                    var column = SerializerUtils.TryToBuildObject<IFakeColumn>(columnType, new object[] { table, columnName, columnRecordType });

                    // Force change Column Name
                    column.Name = columnName;

                    // All Record for this Column
                    foreach (var recordJSON in columnJSON[FakeDbConstants.LabelRecord])
                    {
                        var index = recordJSON[FakeDbConstants.PropertyIndex].Value<int>();
                        var value = recordJSON[FakeDbConstants.PropertyValue].Value<string>();

                        // Add Record to Column
                        column.Add(new KeyValuePair<int, object>(index, value));
                    }

                    // Add Column to Table
                    table.AddColumn(column);
                }

                // Add Table to Database
                database.Add(table);
            }
        }

        public void WriteDatabase(JsonWriter writer, IFakeDatabase database)
        {
            // Start Document
            writer.WriteStartObject(); // Database object
            {
                writer.WritePropertyName(FakeDbConstants.LabelDatabase); writer.WriteValue(""); // Write Database label
                writer.WritePropertyName(FakeDbConstants.PropertyName); writer.WriteValue(database.Name); // Database Name
                writer.WritePropertyName(FakeDbConstants.PropertyClass); writer.WriteValue(database.GetType().AssemblyQualifiedName); // Database Class
                {
                    writer.WritePropertyName(FakeDbConstants.LabelTable); // Start writing Tables in form of array
                    writer.WriteStartArray();
                    {
                        database.ToList().ForEach(table =>
                        {
                            writer.WriteStartObject(); // Table object
                            {
                                writer.WritePropertyName(FakeDbConstants.LabelTable); writer.WriteValue(""); // Write Table label
                                writer.WritePropertyName(FakeDbConstants.PropertyClass); writer.WriteValue(table.GetType().AssemblyQualifiedName); // Table Class
                                writer.WritePropertyName(FakeDbConstants.PropertyName); writer.WriteValue(table.Name); // Table Name
                                writer.WritePropertyName(FakeDbConstants.PropertyColumns); writer.WriteValue(table.ColumnCollection.Count); // Columns Count
                                writer.WritePropertyName(FakeDbConstants.PropertyRows); writer.WriteValue(table.RowCollection.Count); // Rows Count
                                writer.WritePropertyName(FakeDbConstants.LabelColumn);
                                writer.WriteStartArray();
                                {
                                    table.ColumnCollection.ToList().ForEach(column =>
                                    {
                                        writer.WriteStartObject(); // Table object
                                        {
                                            writer.WritePropertyName(FakeDbConstants.LabelColumn); writer.WriteValue(""); // Write Column label
                                            writer.WritePropertyName(FakeDbConstants.PropertyClass); writer.WriteValue(column.GetType().AssemblyQualifiedName); // Column Class
                                            writer.WritePropertyName(FakeDbConstants.PropertyName); writer.WriteValue(column.Name); // Column Name
                                            writer.WritePropertyName(FakeDbConstants.PropertyCount); writer.WriteValue(column.Count); // Column Count
                                            writer.WritePropertyName(FakeDbConstants.PropertyColumnRecordType); writer.WriteValue(column.Type.AssemblyQualifiedName); // Column Record Type
                                            writer.WritePropertyName(FakeDbConstants.LabelRecord);
                                            writer.WriteStartArray();
                                            {
                                                column.ToList().ForEach(record =>
                                                {
                                                    writer.WriteStartObject(); // Column Record object
                                                    {
                                                        writer.WritePropertyName(FakeDbConstants.LabelRecord); writer.WriteValue(""); // Write Record label
                                                        writer.WritePropertyName(FakeDbConstants.PropertyIndex); writer.WriteValue(record.Key.ToString()); // Record Index
                                                        writer.WritePropertyName(FakeDbConstants.PropertyValue); writer.WriteValue(record.Value.ToString()); // Record Value
                                                    }
                                                    writer.WriteEndObject();
                                                });
                                            }
                                            writer.WriteEndArray();
                                        }
                                        writer.WriteEndObject();
                                    });
                                }
                                writer.WriteEndArray();
                            }
                            writer.WriteEndObject();
                        });
                    }
                    writer.WriteEndArray();
                }
            }
            writer.WriteEndObject();
        }

        /// <summary>
        /// Returns read <see cref="Type"/> of given <see cref="JObject"/>.
        /// </summary>
        private Type GetTypeFromProperty(JObject jobject)
        {
            var jobjectClass = jobject.Property(FakeDbConstants.PropertyClass).Value.ToString();
            Type type = Type.GetType(jobjectClass);
            return type;
        }
    }
}