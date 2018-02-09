using KD.FakeDb.Serialization;
using KD.FakeDb.Serialization.XML;
using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Xunit;

namespace KD.FakeDb.XUnitTests.XMLTests
{
    public class FakeDbXMLSerializerTests
    {
        public const string PATH = "db.xml";

        [Fact]
        public void Try_to_write_Database_to_XML()
        {
            try
            {
                File.Delete(PATH);
            }
            catch (Exception) { }

            var db = FakeDatabaseData.GetDatabaseWithData();
            var fileStream = new FileStream(PATH, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
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
            // Pre-generate test file and fill it with data.
            Try_to_write_Database_to_XML();

            var fileStream = new FileStream(PATH, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);

            var serializer = new FakeDbSerializer<XmlReader, XmlWriter>()
            {
                Configuration = new FakeDbXMLByColumnConfiguration()
            };
            serializer.ReadDatabase(XDocument.Load(fileStream).CreateReader());

            var db = serializer.Database;

            Assert.True(db != null);
            Assert.Equal("Test Database", db.Name);
            Assert.True(db.Tables.Count() == 2);
            Assert.Equal("Poland", db["Countries"]["CountryName"][0]);
        }
    }
}