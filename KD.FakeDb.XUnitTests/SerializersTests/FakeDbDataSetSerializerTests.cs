using KD.FakeDb.Factory;
using KD.FakeDb.Serialization;
using KD.FakeDb.Serialization.DataSet;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using Xunit;

namespace KD.FakeDb.XUnitTests.SerializersTests
{
    public class FakeDbDataSetSerializerTests
    {
        [Fact]
        public void Test_if_IFakeDatabase_was_saved_to_file_using_DataSet()
        {
            var db = FakeDatabaseData.GetDatabaseWithData();
            var fileStream = new FileStream("db_DataSet.xml", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
            using (var writer = XmlWriter.Create(fileStream))
            {
                var serializer = new FakeDbSerializer<XmlReader, XmlWriter>()
                {
                    Database = db,
                    Configuration = new FakeDbDataSetConfiguration()
                };
                serializer.WriteDatabase(writer);
            }

            Assert.True(fileStream != null);
        }

        [Fact]
        public void Try_to_read_IFakeDatabase_from_file_using_DataSet()
        {
            var fileStream = new FileStream("db_DataSet.xml", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);

            var serializer = new FakeDbSerializer<XmlReader, XmlWriter>()
            {
                Database = FakeDatabaseFactory.NewDatabase("Some random Database name that will be replaced after fill from DataSet."),
                Configuration = new FakeDbDataSetConfiguration()
            };
            serializer.ReadDatabase(XDocument.Load(fileStream).CreateReader());

            var db = serializer.Database;

            Assert.True(db != null);
            Assert.Equal("Test Database", db.Name);
            Assert.True(db.TableCollection.Count == 2);
            Assert.Equal("Poland", db["Countries"]["CountryName"][0]);
        }
    }
}