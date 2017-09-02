namespace KD.FakeDb.Serialization
{
    /// <summary>
    /// Contains configuration which will be used in <see cref="FakeDbSerializer{TReader, TWriter}"/> to read / write <see cref="IFakeDatabase"/>.
    /// </summary>
    public interface IFakeDbSerializerConfiguration<TReader, TWriter>
    {
        /// <summary>
        /// Reads <see cref="IFakeDatabase"/> using specified <see cref="TReader"/>.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="database"></param>
        void ReadDatabase(TReader reader, ref IFakeDatabase database);
        /// <summary>
        /// Writes <see cref="IFakeDatabase"/> using specified <see cref="TWriter"/>.
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="database"></param>
        void WriteDatabase(TWriter writer, IFakeDatabase database);
    }
}