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

                fakeRow = accTable.GetRow(1);
                fakeRow["AccountId"] = Guid.NewGuid();
                fakeRow["FirstName"] = "Esteban";
                fakeRow["LastName"] = "Hulio";
                fakeRow["CountryName"] = "Spain";

                fakeRow = accTable.GetRow(2);
                fakeRow["AccountId"] = Guid.NewGuid();
                fakeRow["FirstName"] = "A";
                fakeRow["LastName"] = "B";
                fakeRow["CountryName"] = "C";

                fakeRow = accTable.GetRow(3);
                fakeRow["AccountId"] = Guid.NewGuid();
                fakeRow["FirstName"] = "123";
                fakeRow["LastName"] = "321";
                fakeRow["CountryName"] = "123";
            }

            var countryTable = db.AddTable("Countries");
            countryTable.AddColumn("CountryId", typeof(Guid));
            countryTable.AddColumn("CountryName", typeof(string));
            {
                var fakeRow = countryTable.GetRow(0);
                fakeRow["CountryId"] = Guid.NewGuid();
                fakeRow["CountryName"] = "Poland";

                fakeRow = countryTable.GetRow(1);
                fakeRow["CountryId"] = Guid.NewGuid();
                fakeRow["CountryName"] = "Spain";

                fakeRow = countryTable.GetRow(2);
                fakeRow["CountryId"] = Guid.NewGuid();
                fakeRow["CountryName"] = "Germany";

                fakeRow = countryTable.GetRow(3);
                fakeRow["CountryId"] = Guid.NewGuid();
                fakeRow["CountryName"] = "USA";
            }

            return db;
        }
    }
}