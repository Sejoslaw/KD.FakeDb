namespace KD.FakeDb.Factory
{
    /// <summary>
    /// Static class which is used to create new objects dynamically.
    /// </summary>
    public class FakeDatabaseFactory
    {
        /// <summary>
        /// Just to prevent from creating an object of this class.
        /// </summary>
        private FakeDatabaseFactory()
        {
        }

        /// <summary>
        /// Returns new instance of <see cref="IFakeDatabase"/>.
        /// </summary>
        /// <returns></returns>
        public static IFakeDatabase NewDatabase()
        {
            return new FakeDatabase();
        }
    }
}