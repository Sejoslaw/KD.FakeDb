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
            accTable.AddColumn("AccountId", typeof(Guid));
            accTable.AddColumn("FirstName", typeof(string));
            accTable.AddColumn("LastName", typeof(string));
            accTable.AddColumn("CountryName", typeof(string));
            {
                var fakeRow = accTable.GetRow(0);
                fakeRow["AccountId"] = Guid.NewGuid();
                fakeRow["FirstName"] = "Krzysztof";
                fakeRow["LastName"] = "Dobrzynski";
                fakeRow["CountryName"] = "Poland";
            }

            var countryTable = db.AddTable("Countries");
            countryTable.AddColumn("CountryId", typeof(Guid));
            countryTable.AddColumn("CountryName", typeof(string));
            {
                var fakeRow = countryTable.GetRow(0);
                fakeRow["CountryId"] = Guid.NewGuid();
                fakeRow["CountryName"] = "Poland";
            }

            return db;
        }
    }
}