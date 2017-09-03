using Newtonsoft.Json;

namespace KD.FakeDb.Serialization.JSON
{
    /// <summary>
    /// JSON configuration for <see cref="FakeDbSerializer{TReader, TWriter}"/>.
    /// </summary>
    public interface IFakeDbJSONConfiguration : IFakeDbSerializerConfiguration<JsonReader, JsonWriter>
    {
    }
}