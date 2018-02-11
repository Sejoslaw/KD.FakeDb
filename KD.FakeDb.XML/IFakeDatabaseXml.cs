using System.IO;

namespace KD.FakeDb.XML
{
    /// <summary>
    /// <see cref="IFakeDatabaseXml"/> is an implementation of <see cref="IFakeDatabase"/> which base on XML files.
    /// </summary>
    public interface IFakeDatabaseXml : IFakeDatabase
    {
        /// <summary>
        /// Main directory where database stores it's tables.
        /// </summary>
        DirectoryInfo DatabaseDirectory { get; }
    }
}