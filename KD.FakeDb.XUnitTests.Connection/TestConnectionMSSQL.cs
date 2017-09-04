using KD.FakeDb.Connection.MSSQL;
using KD.FakeDb.Factory;
using KD.FakeDb.Serialization;
using KD.FakeDb.Serialization.XML;
using System.Data.SqlClient;
using System.IO;
using System.Xml;
using Xunit;

namespace KD.FakeDb.XUnitTests.Connection
{
    public class TestConnectionMSSQL
    {
        [Fact]
        public void Test_if_database_was_mapped_to_Fake_and_saved_in_XML()
        {
            // Test for SQL Server 13.0.4422.0
            var dbConn = new DatabaseConnectionMSSQL()
            {
                Database = FakeDatabaseFactory.NewDatabase("Name_which_will_be_replaced_after_mapping"),
                Connection = new SqlConnection()
                {
                    ConnectionString = $"" +
                        $"Server=mssql6.gear.host;" +
                        $"Database=testdb49;" +
                        $"User Id=testdb49;" +
                        $"Password=;"
                }
            };

            dbConn.ToFake("testdb49"); // This weird name is an actual Database Name

            var fakeDb = dbConn.Database;

            var fileStream = new FileStream("db_mssql.xml", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
            using (var writer = XmlWriter.Create(fileStream))
            {
                var serializer = new FakeDbSerializer<XmlReader, XmlWriter>()
                {
                    Database = fakeDb,
                    Configuration = new FakeDbXMLByColumnConfiguration()
                };
                serializer.WriteDatabase(writer);
            }

            Assert.True(fakeDb.TableCollection.Count > 0);
        }
    }
}