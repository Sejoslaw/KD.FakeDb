using KD.FakeDb.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace KD.FakeDb.XUnitTests.DatabaseLinqTests
{
    public class DatabaseLinqJoinTests
    {
        [Fact]
        public void Try_to_get_joined_row()
        {
            var db = FakeDatabaseData.GetDatabaseWithData();

            var joined = db["Accounts"][0].ToJoinedRow();
            Assert.Equal("Krzysztof", joined["FirstName"]);

            var toJoin = db["Countries"][0].ToJoinedRow();
            Assert.Equal(2, toJoin.Values.Values.Count);
            Assert.NotNull(toJoin["CountryId"]);

            var afterJoin = joined.JoinToNew(toJoin);

            Assert.Equal(toJoin["CountryId"], afterJoin["CountryId"]);
        }
    }
}