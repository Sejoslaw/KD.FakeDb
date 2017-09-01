using Newtonsoft.Json;

namespace KD.FakeDb.JSON
{
    /// <summary>
    /// Contains configuration which will be used in <see cref="FakeDbJSONSerializer"/> to read / write <see cref="IFakeDatabase"/>.
    /// </summary>
    public interface IFakeDbJSONConfiguration
    {
        /// <summary>
        /// Reads <see cref="IFakeDatabase"/> using specified <see cref="JsonReader"/>.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="database"></param>
        void ReadJSON(JsonReader reader, ref IFakeDatabase database);
        /// <summary>
        /// Writes <see cref="IFakeDatabase"/> using specified <see cref="JsonWriter"/>.
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="database"></param>
        void WriteJSON(JsonWriter writer, IFakeDatabase database);
    }
}