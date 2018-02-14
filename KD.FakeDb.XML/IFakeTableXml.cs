using System;
using System.IO;

namespace KD.FakeDb.XML
{
    /// <summary>
    /// <see cref="IFakeTableXml"/> is an implementation of <see cref="IFakeTable"/> base on XML files.
    /// When the <see cref="IFakeTableXml"/> is disposing it is saved to XML file.
    /// </summary>
    public interface IFakeTableXml : IFakeTable, IDisposable
    {
        /// <summary>
        /// XML file in which data about this table are stored.
        /// </summary>
        FileInfo TableFile { get; }
    }
}