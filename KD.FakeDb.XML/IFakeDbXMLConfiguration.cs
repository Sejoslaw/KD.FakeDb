using System.Xml;

namespace KD.FakeDb.XML
{
    /// <summary>
    /// Contains configuration which will be used in <see cref="FakeDbXMLSerializer"/> to read / write <see cref="IFakeDatabase"/>.
    /// </summary>
    public interface IFakeDbXMLConfiguration
    {
        /// <summary>
        /// Reads <see cref="IFakeDatabase"/> using specified <see cref="XmlReader"/>.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="database"></param>
        void ReadXML(XmlReader reader, ref IFakeDatabase database);
        /// <summary>
        /// Writes <see cref="IFakeDatabase"/> using specified <see cref="XmlWriter"/>.
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="database"></param>
        void WriteXML(XmlWriter writer, IFakeDatabase database);
    }
}