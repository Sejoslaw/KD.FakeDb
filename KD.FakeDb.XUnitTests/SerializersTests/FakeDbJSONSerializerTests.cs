using KD.FakeDb.Serialization;
using KD.FakeDb.Serialization.JSON;
using Newtonsoft.Json;
using System.IO;
using Xunit;

namespace KD.FakeDb.XUnitTests.JSONTests
{
    public class FakeDbJSONSerializerTests
    {
        public const string PATH = "db.json";

        [Fact]
        public void Try_to_write_Database_to_JSON()
        {
            File.Delete(PATH);

            var db = FakeDatabaseData.GetDatabaseWithData();

            using (JsonWriter writer = new JsonTextWriter(File.CreateText(PATH)))
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
            // Pre-generate test file and fill it with data.
            Try_to_write_Database_to_JSON();

            using (JsonReader reader = new JsonTextReader(File.OpenText(PATH)))
            {
                var serializer = new FakeDbSerializer<JsonReader, JsonWriter>()
                {
                    Configuration = new FakeDbJSONByColumnConfiguration()
                };
                serializer.ReadDatabase(reader);

                var db = serializer.Database;

                Assert.True(db != null);
                Assert.Equal("Test Database", db.Name);
                Assert.True(db.TableCollection.Count == 2);
                Assert.Equal("Poland", db["Countries"]["CountryName"][0]);
            }
        }
    }
}