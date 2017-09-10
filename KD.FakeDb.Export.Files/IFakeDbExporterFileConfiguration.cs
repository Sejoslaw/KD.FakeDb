using System.IO;

namespace KD.FakeDb.Export.Files
{
    /// <summary>
    /// <see cref="FakeDbExporter{TStream}"/>'s configuration using <see cref="FileStream"/>.
    /// </summary>
    public interface IFakeDbExporterFileConfiguration : IFakeDbExporterConfiguration<FileStream>
    {
    }
}