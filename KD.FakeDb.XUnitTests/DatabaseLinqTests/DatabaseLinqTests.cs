using KD.FakeDb.Linq;
using System.Linq;
using Xunit;

namespace KD.FakeDb.XUnitTests.DatabaseLinqTests
{
    public class DatabaseLinqTests
    {
        [Fact]
        public void Check_if_Table_Select_returned_Right_record()
        {
            var db = FakeDatabaseData.GetDatabaseWithData();
            var rows = db["Accounts"].Select<IFakeTable, IFakeRow>(row => row["FirstName"].Equals("Krzysztof"));

            Assert.Equal(1, rows.Count());
        }
    }
}