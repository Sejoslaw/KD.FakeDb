using System.Xml;

namespace KD.FakeDb.Serialization.DataSet
{
    /// <summary>
    /// <see cref="System.Data.DataSet"/> configuration for <see cref="FakeDbSerializer{TReader, TWriter}"/>.
    /// </summary>
    public interface IFakeDbDataSetConfiguration : IFakeDbSerializerConfiguration<XmlReader, XmlWriter>
    {
    }
}