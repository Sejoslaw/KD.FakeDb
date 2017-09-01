using KD.FakeDb.Linq;
using Newtonsoft.Json;

namespace KD.FakeDb.JSON
{
    /// <summary>
    /// Configuration which will save each <see cref="IFakeDatabase"/> by it's columns.
    /// </summary>
    public class FakeDbJSONByColumnConfiguration : IFakeDbJSONConfiguration
    {
        public void ReadJSON(JsonReader reader, ref IFakeDatabase database)
        {
        }

        public void WriteJSON(JsonWriter writer, IFakeDatabase database)
        {
            // Start Document
            writer.WriteStartObject(); // Database object
            {
                writer.WritePropertyName(FakeDbJSONConstants.LabelDatabase); writer.WriteValue(""); // Write Database label
                writer.WritePropertyName(FakeDbJSONConstants.PropertyClass); writer.WriteValue(database.GetType().AssemblyQualifiedName); // Database Class
                {
                    writer.WritePropertyName(FakeDbJSONConstants.LabelTable); // Start writing Tables in form of array
                    writer.WriteStartArray();
                    {
                        database.ForEach(table =>
                        {
                            writer.WriteStartObject(); // Table object
                            {
                                writer.WritePropertyName(FakeDbJSONConstants.LabelTable); writer.WriteValue(""); // Write Table label
                                writer.WritePropertyName(FakeDbJSONConstants.PropertyClass); writer.WriteValue(table.GetType().AssemblyQualifiedName); // Table Class
                                writer.WritePropertyName(FakeDbJSONConstants.PropertyName); writer.WriteValue(table.Name); // Table Name
                                writer.WritePropertyName(FakeDbJSONConstants.PropertyColumns); writer.WriteValue(table.ColumnCollection.Count); // Columns Count
                                writer.WritePropertyName(FakeDbJSONConstants.PropertyRows); writer.WriteValue(table.RowCollection.Count); // Rows Count
                                writer.WritePropertyName(FakeDbJSONConstants.LabelColumn);
                                writer.WriteStartArray();
                                {
                                    table.ColumnCollection.ForEach(column =>
                                    {
                                        writer.WriteStartObject(); // Table object
                                        {
                                            writer.WritePropertyName(FakeDbJSONConstants.LabelColumn); writer.WriteValue(""); // Write Column label
                                            writer.WritePropertyName(FakeDbJSONConstants.PropertyClass); writer.WriteValue(column.GetType().AssemblyQualifiedName); // Column Class
                                            writer.WritePropertyName(FakeDbJSONConstants.PropertyName); writer.WriteValue(column.Name); // Column Name
                                            writer.WritePropertyName(FakeDbJSONConstants.PropertyCount); writer.WriteValue(column.Count); // Column Count
                                            writer.WritePropertyName(FakeDbJSONConstants.PropertyColumnRecordType); writer.WriteValue(column.Type.AssemblyQualifiedName); // Column Record Type
                                            writer.WritePropertyName(FakeDbJSONConstants.LabelRecord);
                                            writer.WriteStartArray();
                                            {
                                                column.ForEach(record =>
                                                {
                                                    writer.WriteStartObject(); // Column Record object
                                                    {
                                                        writer.WritePropertyName(FakeDbJSONConstants.LabelRecord); writer.WriteValue(""); // Write Record label
                                                        writer.WritePropertyName(FakeDbJSONConstants.PropertyIndex); writer.WriteValue(record.Key.ToString()); // Record Index
                                                        writer.WritePropertyName(FakeDbJSONConstants.PropertyValue); writer.WriteValue(record.Value.ToString()); // Record Value
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