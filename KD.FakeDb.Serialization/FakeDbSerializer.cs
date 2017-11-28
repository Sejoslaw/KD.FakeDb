using System;

namespace KD.FakeDb.Serialization
{
    /// <summary>
    /// Serializer used for <see cref="IFakeDatabase"/>.
    /// Single file should contains only one <see cref="IFakeDatabase"/>.
    /// </summary>
    /// <typeparam name="TReader"> Reader type. </typeparam>
    /// <typeparam name="TWriter"> Writer type. </typeparam>
    public class FakeDbSerializer<TReader, TWriter>
    {
        private IFakeDatabase _database;
        private IFakeDbSerializerConfiguration<TReader, TWriter> _configuration;

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
        public IFakeDbSerializerConfiguration<TReader, TWriter> Configuration
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
        /// Returns <see cref="Type"/> of a reader.
        /// </summary>
        public Type ReaderType
        {
            get
            {
                return typeof(TReader);
            }
        }
        /// <summary>
        /// Returns <see cref="Type"/> of w writer.
        /// </summary>
        public Type WriterType
        {
            get
            {
                return typeof(TWriter);
            }
        }

        /// <summary>
        /// Reads <see cref="IFakeDatabase"/> using given reader with specified <see cref="IFakeDbSerializerConfiguration{TReader, TWriter}"/>.
        /// </summary>
        /// <param name="reader"> Reader which will be used to read <see cref="IFakeDatabase"/>. </param>
        public void ReadDatabase(TReader reader)
        {
            this.Configuration.ReadDatabase(reader, this._database);
        }

        /// <summary>
        /// Writes <see cref="IFakeDatabase"/> using given writer with specified <see cref="IFakeDbSerializerConfiguration{TReader, TWriter}"/>.
        /// </summary>
        /// <param name="writer"> Writer which will be used to write <see cref="IFakeDatabase"/>. </param>
        public void WriteDatabase(TWriter writer)
        {
            this.Configuration.WriteDatabase(writer, this._database);
        }
    }
}