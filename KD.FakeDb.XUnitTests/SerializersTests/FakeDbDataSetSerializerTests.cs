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
        public const string PATH = "db_DataSet.xml";

        [Fact]
        public void Test_if_IFakeDatabase_was_saved_to_file_using_DataSet()
        {
            File.Delete(PATH);

            var db = FakeDatabaseData.GetDatabaseWithData();
            var fileStream = new FileStream(PATH, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
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
            // Pre-generate test file and fill it with data.
            Test_if_IFakeDatabase_was_saved_to_file_using_DataSet();

            var fileStream = new FileStream(PATH, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);

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