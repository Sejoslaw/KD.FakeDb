using KD.FakeDb.Serialization;
using KD.FakeDb.XML;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using Xunit;

namespace KD.FakeDb.XUnitTests.XMLTests
{
    public class FakeDbXMLSerializerTests
    {
        [Fact]
        public void Try_to_write_Database_to_XML()
        {
            var db = FakeDatabaseData.GetDatabaseWithData();
            var fileStream = new FileStream("db.xml", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
            using (var writer = XmlWriter.Create(fileStream))
            {
                var serializer = new FakeDbSerializer<XmlReader, XmlWriter>()
                {
                    Database = db,
                    Configuration = new FakeDbXMLByColumnConfiguration()
                };
                serializer.WriteDatabase(writer);
            }

            Assert.True(fileStream != null);
        }

        [Fact]
        public void Try_to_read_Database_from_XML()
        {
            var fileStream = new FileStream("db.xml", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);

            var serializer = new FakeDbSerializer<XmlReader, XmlWriter>()
            {
                Configuration = new FakeDbXMLByColumnConfiguration()
            };
            serializer.ReadDatabase(XDocument.Load(fileStream).CreateReader());

            var db = serializer.Database;

            Assert.True(db != null);
            Assert.True(db.TableCollection.Count == 2);
            Assert.Equal("Poland", db["Countries"]["CountryName"][0]);
        }
    }
}