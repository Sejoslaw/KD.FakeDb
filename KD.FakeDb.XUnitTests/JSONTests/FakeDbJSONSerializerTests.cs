using KD.FakeDb.JSON;
using KD.FakeDb.Serialization;
using Newtonsoft.Json;
using System.IO;
using Xunit;

namespace KD.FakeDb.XUnitTests.JSONTests
{
    public class FakeDbJSONSerializerTests
    {
        [Fact]
        public void Try_to_write_Database_to_JSON()
        {
            var db = FakeDatabaseData.GetDatabaseWithData();

            using (JsonWriter writer = new JsonTextWriter(File.CreateText("db.json")))
            {
                var serializer = new FakeDbSerializer<JsonReader, JsonWriter>()
                {
                    Database = db,
                    Configuration = new FakeDbJSONByColumnConfiguration()
                };
                serializer.WriteDatabase(writer);
            }

            Assert.True(db != null);
        }

        [Fact]
        public void Try_to_read_Database_from_JSON()
        {
            using (JsonReader reader = new JsonTextReader(File.OpenText("db.json")))
            {
                var serializer = new FakeDbSerializer<JsonReader, JsonWriter>()
                {
                    Configuration = new FakeDbJSONByColumnConfiguration()
                };
                serializer.ReadDatabase(reader);

                var db = serializer.Database;

                Assert.True(db != null);
                Assert.True(db.TableCollection.Count == 2);
                Assert.Equal("Poland", db["Countries"]["CountryName"][0]);
            }
        }
    }
}