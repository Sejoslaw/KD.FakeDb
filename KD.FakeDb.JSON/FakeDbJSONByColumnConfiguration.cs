using KD.FakeDb.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace KD.FakeDb.JSON
{
    /// <summary>
    /// Configuration which will save each <see cref="IFakeDatabase"/> by it's columns.
    /// </summary>
    public class FakeDbJSONByColumnConfiguration : IFakeDbJSONConfiguration
    {
        public void ReadDatabase(JsonReader reader, ref IFakeDatabase database)
        {
            var databaseJSON = JObject.Load(reader); // Load JSON Database object
            var databaseClass = databaseJSON.Property(FakeDbConstants.PropertyClass).Value.ToString(); // Database Type
            Type databaseType = Type.GetType(databaseClass);
        }

        public void WriteDatabase(JsonWriter writer, IFakeDatabase database)
        {
            // Start Document
            writer.WriteStartObject(); // Database object
            {
                writer.WritePropertyName(FakeDbConstants.LabelDatabase); writer.WriteValue(""); // Write Database label
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
    }
}