using Xunit;

namespace KD.FakeDb.XUnitTests.DatabaseTests
{
    public class DatabaseTests
    {
        [Fact]
        public void Test_if_table_record_was_added_successfully()
        {
            var db = FakeDatabaseData.GetDatabaseWithData();
            var accTable = db["Accounts"];

            Assert.True(4 <= accTable.ColumnCollection.Count);
            Assert.True(4 <= accTable.RowCollection.Count);
            Assert.Equal("Krzysztof", accTable.GetRow(0)["FirstName"]);
        }
    }
}