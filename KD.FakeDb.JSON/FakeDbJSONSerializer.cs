using Newtonsoft.Json;

namespace KD.FakeDb.JSON
{
    /// <summary>
    /// JSON serializer used for <see cref="IFakeDatabase"/>.
    /// Single JSON file should contains only one <see cref="IFakeDatabase"/>.
    /// </summary>
    public class FakeDbJSONSerializer
    {
        private IFakeDatabase _database;
        private IFakeDbJSONConfiguration _configuration;

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
        public IFakeDbJSONConfiguration Configuration
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
        /// Reads <see cref="IFakeDatabase"/> using given <see cref="JsonReader"/> with specified <see cref="IFakeDbJSONConfiguration"/>.
        /// </summary>
        /// <param name="reader"></param>
        public void ReadJSON(JsonReader reader)
        {
            CheckConfiguration();

            this.Configuration.ReadJSON(reader, ref this._database);
        }

        /// <summary>
        /// Writes <see cref="IFakeDatabase"/> using given <see cref="JsonWriter"/> with specified <see cref="IFakeDbJSONConfiguration"/>.
        /// </summary>
        /// <param name="writer"></param>
        public void WriteJSON(JsonWriter writer)
        {
            CheckConfiguration();

            this.Configuration.WriteJSON(writer, this._database);
        }

        /// <summary>
        /// This should check everything what could threw error.
        /// </summary>
        private void CheckConfiguration()
        {
            // Set default configuration if user didn't set any.
            if (this.Configuration == null)
            {
                this.Configuration = new FakeDbJSONByColumnConfiguration();
            }
        }
    }
}