using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace KD.FakeDb.XML
{
    /// <summary>
    /// XML serializer used for <see cref="IFakeDatabase"/>.
    /// Single XML file should contains only one <see cref="IFakeDatabase"/>.
    /// </summary>
    public class FakeDbXMLSerializer : IXmlSerializable
    {
        private IFakeDatabase _database;
        private IFakeDbXMLConfiguration _configuration;

        /// <summary>
        /// Database used for serialization.
        /// </summary>
        public IFakeDatabase Database
        {
            get
            {
                return this._database;
            }

            set
            {
                this._database = value;
            }
        }
        /// <summary>
        /// Configuration which will be used while serializing and deserializing <see cref="IFakeDatabase"/>.
        /// </summary>
        public IFakeDbXMLConfiguration Configuration
        {
            get
            {
                return this._configuration;
            }

            set
            {
                this._configuration = value;
            }
        }

        /// <summary>
        /// Reads <see cref="IFakeDatabase"/> using given <see cref="XmlReader"/> with specified <see cref="IFakeDbXMLConfiguration"/>.
        /// </summary>
        /// <param name="reader"></param>
        public void ReadXml(XmlReader reader)
        {
            CheckConfiguration();

            this.Configuration.ReadXML(reader, ref this._database);
        }

        /// <summary>
        /// Writes <see cref="IFakeDatabase"/> using given <see cref="XmlWriter"/> with specified <see cref="IFakeDbXMLConfiguration"/>.
        /// </summary>
        /// <param name="writer"></param>
        public void WriteXml(XmlWriter writer)
        {
            CheckConfiguration();

            this.Configuration.WriteXML(writer, this._database);
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        /// <summary>
        /// This should check evertything what could threw error.
        /// </summary>
        private void CheckConfiguration()
        {
            // Set default configuration if user didn't set any.
            if (this.Configuration == null)
            {
                this.Configuration = new FakeDbXMLByColumnConfiguration();
            }
        }
    }
}