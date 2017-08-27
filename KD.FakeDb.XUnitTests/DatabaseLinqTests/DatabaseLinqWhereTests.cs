using KD.FakeDb.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace KD.FakeDb.XUnitTests.DatabaseLinqTests
{
    public class DatabaseLinqWhereTests
    {
        [Fact]
        public void Check_if_Table_Where_returned_Right_record()
        {
            var db = FakeDatabaseData.GetDatabaseWithData();
            var rows = db["Accounts"].Where(row => row["FirstName"].Equals("Krzysztof"));

            Assert.Equal(1, rows.Count());
        }

        [Fact]
        public void Check_if_Table_with_List_in_Where_returned_Right_record()
        {
            var db = FakeDatabaseData.GetDatabaseWithData();
            var rows = db["Accounts"].Where(new List<Func<IFakeRow, bool>>()
            {
                row => row["FirstName"].Equals("Krzysztof"),
                row => row["LastName"].Equals("Dobrzynski")
            });

            Assert.Equal(1, rows.Count());
        }

        [Fact]
        public void Check_if_multiple_tables_with_same_field_will_be_returned()
        {
            var db = FakeDatabaseData.GetDatabaseWithData();
            var query = db.Where(row => row["CountryName"].Equals("Poland"));

            Assert.Equal(2, query.Count);
            Assert.Equal(1, query[db["Accounts"]].Count);
        }
    }
}