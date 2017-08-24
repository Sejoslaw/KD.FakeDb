using KD.FakeDb.Factory;
using System;
using Xunit;

namespace KD.FakeDb.XUnitTests.DatabaseTests
{
    public class DatabaseTests
    {
        [Fact]
        public void Test_if_table_record_was_added_successfully()
        {
            IFakeDatabase db = FakeDatabaseFactory.NewDatabase();
            IFakeTable accTable = db.AddTable("Account");
            accTable.AddColumn("Id", typeof(Guid));
            accTable.AddColumn("FirstName", typeof(string));
            accTable.AddColumn("LastName", typeof(string));

            // Row is dynamically created when getting
            // Note that before this there wasn't any Row at index 1
            IFakeRow fakeRow = accTable.GetRow(1);
            fakeRow["Id"] = Guid.NewGuid();
            fakeRow["FirstName"] = "Krzysztof";
            fakeRow["LastName"] = "Dobrzynski";

            Assert.Equal(3, accTable.ColumnCollection.Count);
            Assert.Equal(1, accTable.RowCollection.Count);
            Assert.Equal("Krzysztof", accTable.GetRow(1)["FirstName"]);
        }
    }
}