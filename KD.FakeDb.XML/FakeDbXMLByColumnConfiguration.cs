using KD.FakeDb.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace KD.FakeDb.XML
{
    /// <summary>
    /// Configuration which will save each <see cref="IFakeTable"/> by it's columns.
    /// </summary>
    public class FakeDbXMLByColumnConfiguration : IFakeDbXMLConfiguration
    {
        public void ReadDatabase(XmlReader reader, ref IFakeDatabase database)
        {
            // XML Document
            var document = XDocument.Load(reader);

            // Create Database
            var databaseElements = document.Descendants(XName.Get(FakeDbConstants.LabelDatabase));

            if (!databaseElements.Any())
            {
                throw new Exception(string.Format("Fake Database not found ({0}).", reader.NamespaceURI));
            }

            var databaseElement = databaseElements.ElementAt(0);
            var databaseClass = databaseElement.Attribute(FakeDbConstants.AttributeClass).Value;
            var databaseType = Type.GetType(databaseClass);

            database = (IFakeDatabase)Activator.CreateInstance(databaseType);

            // All Tables from File
            var tables = document.Descendants(XName.Get(FakeDbConstants.LabelTable));
            foreach (var table in tables)
            {
                // Create Table
                var tableName = table.Attribute(XName.Get(FakeDbConstants.AttributeName)).Value;
                var tableClass = table.Attribute(XName.Get(FakeDbConstants.AttributeClass)).Value;
                var tableType = Type.GetType(tableClass);

                IFakeTable tableToAdd = null;
                try
                {
                    // Default constructor
                    tableToAdd = (IFakeTable)Activator.CreateInstance(tableType);
                }
                catch (Exception)
                {
                    try
                    {
                        // FakeTable parametrized constrcutor
                        tableToAdd = (IFakeTable)Activator.CreateInstance(tableType, new object[] { database, tableName });
                    }
                    catch (Exception)
                    {
                        throw new Exception(string.Format("Error when creating Table from: {0}", table));
                    }
                }

                // Force change the Table Name
                tableToAdd.Name = tableName;

                // All Columns in Table
                var columns = table.Descendants(XName.Get(FakeDbConstants.LabelColumn));
                foreach (var column in columns)
                {
                    // Create Column
                    var columnName = column.Attribute(XName.Get(FakeDbConstants.AttributeName)).Value;
                    var columnClass = column.Attribute(XName.Get(FakeDbConstants.AttributeClass)).Value;
                    var columnType = Type.GetType(columnClass);

                    var columnRecordClass = column.Attribute(XName.Get(FakeDbConstants.AttributeColumnRecordType)).Value;
                    var columnRecordType = Type.GetType(columnRecordClass);

                    IFakeColumn columnToAdd = null;
                    try
                    {
                        // Default constructor
                        columnToAdd = (IFakeColumn)Activator.CreateInstance(columnType);
                    }
                    catch (Exception)
                    {
                        try
                        {
                            // FakeColumn parametrized constructor
                            columnToAdd = (IFakeColumn)Activator.CreateInstance(columnType, new object[] { tableToAdd, columnName, columnRecordType });
                        }
                        catch (Exception)
                        {
                            throw new Exception(string.Format("Error when creating Column from: {0}", column));
                        }
                    }

                    // Force change Column Name
                    columnToAdd.Name = columnName;

                    // All Records for this Column
                    var records = column.Descendants(XName.Get(FakeDbConstants.LabelRecord));
                    foreach (var record in records)
                    {
                        var index = record.Attribute(XName.Get(FakeDbConstants.AttributeIndex)).Value;
                        var value = record.Attribute(XName.Get(FakeDbConstants.AttributeValue)).Value;

                        // Add Record to Column
                        columnToAdd.Add(new KeyValuePair<int, object>(Int32.Parse(index), value));
                    }

                    // Add Column to Table
                    tableToAdd.AddColumn(columnToAdd);
                }

                // Add Table to Database
                database.Add(tableToAdd);
            }
        }

        public void WriteDatabase(XmlWriter writer, IFakeDatabase database)
        {
            writer.WriteStartDocument(); // Start Document
            {
                writer.WriteStartElement(FakeDbConstants.LabelDatabase); // Write Database
                writer.WriteAttributeString(FakeDbConstants.AttributeClass, database.GetType().AssemblyQualifiedName); // Database Class
                {
                    database.ToList().ForEach(table =>
                    {
                        writer.WriteStartElement(FakeDbConstants.LabelTable); // Write Table
                        writer.WriteAttributeString(FakeDbConstants.AttributeClass, table.GetType().AssemblyQualifiedName); // Table Class
                        writer.WriteAttributeString(FakeDbConstants.AttributeName, table.Name); // Table Name
                        writer.WriteAttributeString(FakeDbConstants.AttributeColumns, table.ColumnCollection.Count.ToString()); // Columns Count
                        writer.WriteAttributeString(FakeDbConstants.AttributeRows, table.RowCollection.Count.ToString()); // Rows Count
                        {
                            table.ColumnCollection.ToList().ForEach(column =>
                            {
                                writer.WriteStartElement(FakeDbConstants.LabelColumn); // Write Column
                                writer.WriteAttributeString(FakeDbConstants.AttributeClass, column.GetType().AssemblyQualifiedName); // Column Class
                                writer.WriteAttributeString(FakeDbConstants.AttributeName, column.Name); // Column Name
                                writer.WriteAttributeString(FakeDbConstants.AttributeCount, column.Count.ToString()); // Column Count
                                writer.WriteAttributeString(FakeDbConstants.AttributeColumnRecordType, column.Type.AssemblyQualifiedName); // Column Records Type
                                {
                                    column.ToList().ForEach(record =>
                                    {
                                        writer.WriteStartElement(FakeDbConstants.LabelRecord); // Write Record
                                        writer.WriteAttributeString(FakeDbConstants.AttributeIndex, record.Key.ToString()); // Record Index
                                        writer.WriteAttributeString(FakeDbConstants.AttributeValue, record.Value.ToString()); // Record Object
                                        writer.WriteEndElement();
                                    });
                                }
                                writer.WriteEndElement();
                            });
                        }
                        writer.WriteEndElement();
                    });
                }
                writer.WriteEndElement();
            }
            writer.WriteEndDocument();
        }
    }
}