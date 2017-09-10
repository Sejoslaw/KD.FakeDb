using System.IO;

namespace KD.FakeDb.Export
{
    /// <summary>
    /// Exporter used to export <see cref="IFakeDatabase"/>.
    /// </summary>
    public class FakeDbExporter<TStream>
        where TStream : Stream
    {
        /// <summary>
        /// Configuration used by this <see cref="FakeDbExporter{TStream}"/>.
        /// </summary>
        public IFakeDbExporterConfiguration<TStream> Configuration { get; set; }

        /// <summary>
        /// Exports given <see cref="IFakeDatabase"/> using specified <see cref="Stream"/>.
        /// </summary>
        public void Export(TStream stream, IFakeDatabase database)
        {
            this.Configuration.Export(stream, database);
        }
    }
}