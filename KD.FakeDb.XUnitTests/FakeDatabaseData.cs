using KD.FakeDb.Factory;
using System;

namespace KD.FakeDb.XUnitTests
{
    /// <summary>
    /// Class which contains some pre-build database with custom data.
    /// </summary>
    public class FakeDatabaseData
    {
        private FakeDatabaseData()
        {
        }

        /// <summary>
        /// Returns new <see cref="IFakeDatabase"/> with some pre-build data.
        /// </summary>
        /// <returns></returns>
        public static IFakeDatabase GetDatabaseWithData()
        {
            var db = FakeDatabaseFactory.NewDatabase();

            var accTable = db.AddTable("Accounts");

            accTable.AddColumn("Id", typeof(Guid));
            accTable.AddColumn("FirstName", typeof(string));
            accTable.AddColumn("LastName", typeof(string));
            {
                // Add rows to Account Table
                var fakeRow = accTable.GetRow(0);
                fakeRow["Id"] = Guid.NewGuid();
                fakeRow["FirstName"] = "Krzysztof";
                fakeRow["LastName"] = "Dobrzynski";
            }

            return db;
        }
    }
}