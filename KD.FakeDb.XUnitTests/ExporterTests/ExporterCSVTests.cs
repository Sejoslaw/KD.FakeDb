using KD.FakeDb.Export;
using KD.FakeDb.Export.Files.CSV;
using System;
using System.IO;
using Xunit;

namespace KD.FakeDb.XUnitTests.ExporterTests
{
    public class ExporterCSVTests
    {
        public const string PATH = "db.csv";

        [Fact]
        public void Export_IFakeDatabase_to_CSV_File()
        {
            try
            {
                File.Delete(PATH);
            }
            catch (Exception) { }

            var exporter = new FakeDbExporter<FileStream>()
            {
                Configuration = new FakeDbExporterCSVConfiguration()
            };

            var fileStream = new FileStream(PATH, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
            var fakeDb = FakeDatabaseData.GetDatabaseWithData();
            exporter.Export(fileStream, fakeDb);

            Assert.True(fileStream != null);
        }
    }
}