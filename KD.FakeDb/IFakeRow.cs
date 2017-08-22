namespace KD.FakeDb
{
    /// <summary>
    /// Describes single Row in <see cref="IFakeTable"/>.
    /// </summary>
    public interface IFakeRow
    {
        /// <summary>
        /// Returns the value in this <see cref="IFakeRow"/> from specified <see cref="IFakeColumn"/>. If column don't exists <see cref="System.ArgumentException"/> wil be thrown.
        /// </summary>
        /// <param name="columnName"> Name of the <see cref="IFakeColumn"/> from which to return value for this <see cref="IFakeRow"/>. </param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException"></exception>
        object this[string columnName] { get; set; }

        /// <summary>
        /// Returns the Index in <see cref="IFakeTable"/> of this <see cref="IFakeRow"/>.
        /// </summary>
        int Index { get; }

        /// <summary>
        /// Returns the number of elements in this <see cref="IFakeRow"/>. This should be equal with <see cref="IFakeColumnCollection.Count"/>.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Returns the <see cref="IFakeTable"/> which contains this <see cref="IFakeRow"/>.
        /// </summary>
        IFakeTable Table { get; }
    }
}