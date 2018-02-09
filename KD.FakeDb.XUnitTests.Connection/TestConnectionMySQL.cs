using KD.FakeDb.Connection.MySQL;
using KD.FakeDb.Factory;
using KD.FakeDb.Serialization;
using KD.FakeDb.Serialization.XML;
using MySql.Data.MySqlClient;
using System.IO;
using System.Linq;
using System.Xml;
using Xunit;

namespace KD.FakeDb.XUnitTests.Connection
{
    public class TestConnectionMySQL
    {
        [Fact]
        public void Test_if_database_was_mapped_to_Fake_and_saved_in_XML()
        {
            var dbConn = new DatabaseConnectionMySQL()
            {
                Database = FakeDatabaseFactory.NewDatabase("Name_which_will_be_replaced_after_mapping"),
                Connection = new MySqlConnection()
                {
                    ConnectionString = $"" +
                        $"Data Source=sql11.freemysqlhosting.net;" +
                        $"Initial Catalog=INFORMATION_SCHEMA;" + // Start in Database where all required informations about all Databases on Server are stored
                        $"User id=sql11192873;" +
                        $"Password=;"
                }
            };

            dbConn.ToFake("sql11192873"); // This weird name is an actual Database Name

            var fakeDb = dbConn.Database;

            var fileStream = new FileStream("db_mysql.xml", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
            using (var writer = XmlWriter.Create(fileStream))
            {
                var serializer = new FakeDbSerializer<XmlReader, XmlWriter>()
                {
                    Database = fakeDb,
                    Configuration = new FakeDbXMLByColumnConfiguration()
                };
                serializer.WriteDatabase(writer);
            }

            Assert.True(fakeDb.Tables.Count() > 0);
        }
    }
}