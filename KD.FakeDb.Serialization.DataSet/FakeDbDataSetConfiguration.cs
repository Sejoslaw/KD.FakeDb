using KD.FakeDb.Converter.DataSet;
using System.Xml;

namespace KD.FakeDb.Serialization.DataSet
{
    public class FakeDbDataSetConfiguration : IFakeDbDataSetConfiguration
    {
        public void ReadDatabase(XmlReader reader, IFakeDatabase database)
        {
            // Create DataSet
            var dataSet = new System.Data.DataSet();

            // Read XML and fill DataSet
            dataSet.ReadXml(reader);

            // Convert data from DataSet to Fake Database
            dataSet.ToFakeDatabase(database);
        }

        public void WriteDatabase(XmlWriter writer, IFakeDatabase database)
        {
            // Create DataSet using Converter
            var dataSet = database.ToDataSet();

            // Write XML File
            dataSet.WriteXml(writer);
        }
    }
}