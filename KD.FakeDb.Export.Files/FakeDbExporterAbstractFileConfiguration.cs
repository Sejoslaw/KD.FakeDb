using System;
using System.IO;

namespace KD.FakeDb.Export.Files
{
    /// <summary>
    /// Abstract File Configuration for <see cref="FakeDbExporter{TStream}"/>.
    /// Does all pre-checking before actual export happens.
    /// </summary>
    public abstract class FakeDbExporterAbstractFileConfiguration : IFakeDbExporterFileConfiguration
    {
        public void Export(FileStream stream, IFakeDatabase database)
        {
            // Every Exporter which exports to File needs Write permission
            if (!stream.CanWrite)
            {
                throw new Exception("FakeDbExporter needs Write permission.");
            }

            // Actual method
            ExportToFile(stream, database);
        }

        /// <summary>
        /// Actual method which writes to <see cref="File"/>.
        /// </summary>
        internal abstract void ExportToFile(FileStream stream, IFakeDatabase database);
    }
}