using System.IO;

namespace KD.FakeDb.Export
{
    /// <summary>
    /// Configuration used by <see cref="FakeDbExporter{TStream}"/> to export <see cref="IFakeDatabase"/>.
    /// </summary>
    public interface IFakeDbExporterConfiguration<TStream>
        where TStream : Stream
    {
        /// <summary>
        /// Export given <see cref="IFakeDatabase"/> using specified <see cref="Stream"/>.
        /// </summary>
        void Export(TStream stream, IFakeDatabase database);
    }
}