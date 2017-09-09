using KD.FakeDb.Converter.DataSet;
using Xunit;

namespace KD.FakeDb.XUnitTests.ConvertersTests
{
    public class ToDataSetConverterTests
    {
        [Fact]
        public void Test_if_IFakeDatabase_was_converted_to_DataSet()
        {
            var fakeDb = FakeDatabaseData.GetDatabaseWithData();

            var dataSet = fakeDb.ToDataSet();

            Assert.True(dataSet.Tables.Count >= 2);
            Assert.True(dataSet.Tables["Accounts"].Columns.Count >= 4);
            Assert.True(dataSet.Tables["Accounts"].Rows.Count >= 4);
            Assert.Equal(dataSet.Tables["Accounts"].Rows[0]["FirstName"], "Krzysztof");
        }
    }
}