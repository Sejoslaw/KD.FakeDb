using KD.FakeDb.Factory;
using KD.FakeDb.Serialization;
using KD.FakeDb.Serialization.DataSet;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml;
using System.Xml.Linq;
using Xunit;

namespace KD.FakeDb.XUnitTests.SerializersTests
{
    public class FakeDbDataSetSerializerTests
    {
        public const string PATH = "db_DataSet.xml";
        public const string PATH_2 = "db_DataSet_read_me.xml";

        [Fact]
        public void Test_if_IFakeDatabase_was_saved_to_file_using_DataSet()
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
                    Configuration = new FakeDbDataSetConfiguration()
                };
                serializer.WriteDatabase(writer);
            }

            Assert.True(fileStream != null);
        }

        [Fact]
        public void Try_to_read_IFakeDatabase_from_file_using_DataSet()
        {
            try
            {
                File.Delete(PATH_2);
            }
            catch (Exception) { }

            var db = FakeDatabaseData.GetDatabaseWithData();
            var fileStream = new FileStream(PATH_2, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);

            var serializer = new FakeDbSerializer<XmlReader, XmlWriter>()
            {
                Database = db,
                Configuration = new FakeDbDataSetConfiguration()
            };

            lock (new object())
            {
                serializer.WriteDatabase(XmlWriter.Create(fileStream));
                fileStream.Flush();
                fileStream.Close();
            }

            // Prepare to read
            serializer.Database = FakeDatabaseFactory.NewDatabase("New empty Database for Read Test");
            fileStream = new FileStream(PATH_2, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
            serializer.ReadDatabase(XmlReader.Create(fileStream));

            var dbReaded = serializer.Database;

            Assert.True(db != null);
            Assert.Equal("Test Database", db.Name);
            Assert.True(db.Tables.Count() == 2);
            Assert.Equal("Poland", db["Countries"]["CountryName"][0]);
        }
    }
}