using System;

namespace KD.FakeDb.Factory
{
    /// <summary>
    /// Static class which is used to create new objects dynamically.
    /// </summary>
    public static class FakeDatabaseFactory
    {
        /// <summary>
        /// Returns new instance of <see cref="IFakeDatabase"/>.
        /// </summary>
        public static IFakeDatabase NewDatabase(string databaseName)
        {
            if (string.IsNullOrEmpty(databaseName))
            {
                throw new Exception($"Database Name is null or empty.({ databaseName })");
            }

            var db = new FakeDatabase()
            {
                Name = databaseName
            };
            return db;
        }
    }
}