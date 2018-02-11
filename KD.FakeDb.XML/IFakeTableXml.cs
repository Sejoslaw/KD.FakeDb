using System.IO;

namespace KD.FakeDb.XML
{
    /// <summary>
    /// <see cref="IFakeTableXml"/> is an implementation of <see cref="IFakeTable"/> base on XML files.
    /// </summary>
    public interface IFakeTableXml : IFakeTable
    {
        /// <summary>
        /// XML file in which data about this table are stored.
        /// </summary>
        FileInfo TableFile { get; }
    }
}