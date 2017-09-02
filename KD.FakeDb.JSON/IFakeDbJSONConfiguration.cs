using KD.FakeDb.Serialization;
using Newtonsoft.Json;

namespace KD.FakeDb.JSON
{
    /// <summary>
    /// JSON configuration for <see cref="FakeDbSerializer{TReader, TWriter}"/>.
    /// </summary>
    public interface IFakeDbJSONConfiguration : IFakeDbSerializerConfiguration<JsonReader, JsonWriter>
    {
    }
}