using KD.FakeDb.Factory;
using Xunit;

namespace KD.FakeDb.XUnitTests.FactoryTests
{
    public class FactoryTests
    {
        [Fact]
        public void Was_Fake_Database_successfully_created()
        {
            var database = FakeDatabaseFactory.NewDatabase("Test Database");

            Assert.NotNull(database);
        }
    }
}