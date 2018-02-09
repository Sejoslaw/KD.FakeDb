using System.Linq;
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

            Assert.True(4 <= accTable.Columns.Count());
            Assert.True(4 <= accTable.Rows.Count());
            Assert.Equal("Krzysztof", accTable.GetRow(0)["FirstName"]);
        }
    }
}