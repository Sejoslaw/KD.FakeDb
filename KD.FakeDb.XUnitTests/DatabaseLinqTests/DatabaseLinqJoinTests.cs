using KD.FakeDb.Linq;
using System;
using Xunit;

namespace KD.FakeDb.XUnitTests.DatabaseLinqTests
{
    public class DatabaseLinqJoinTests
    {
        [Fact]
        public void Try_to_get_joined_row()
        {
            var db = FakeDatabaseData.GetDatabaseWithData();
            var accTable = db["Accounts"];
            var row = accTable[0];

            var joinedRows = row.Join<IFakeRow, IFakeJoinedRow, IFakeDatabase>(db["Countries"], accTable["CountryName"]);
        }
    }
}