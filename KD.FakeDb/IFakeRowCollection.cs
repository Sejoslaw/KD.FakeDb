namespace KD.FakeDb
{
    /// <summary>
    /// Describes a collection of <see cref="IFakeRow"/>'s inside <see cref="IFakeTable"/>.
    /// </summary>
    public interface IFakeRowCollection
    {
        /// <summary>
        /// Returns <see cref="IFakeRow"/> from given Row index if Row exists; otherwise <see cref="System.ArgumentException"/> will be thrown.
        /// </summary>
        /// <param name="rowIndex"> Index of the <see cref="IFakeRow"/>. </param>
        /// <returns> Returns single <see cref="IFakeRow"/> from this <see cref="IFakeTable"/>. </returns>
        IFakeRow this[int rowIndex] { get; }

        /// <summary>
        /// Returns the number of Rows in this collection.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Returns <see cref="IFakeTable"/> to which this collection belongs.
        /// </summary>
        IFakeTable Table { get; }

        /// <summary>
        /// Adds new <see cref="IFakeRow"/> to this <see cref="IFakeTable"/> with given name.
        /// </summary>
        /// <param name="row"> Row which should be added. </param>
        /// <returns> Returns true if Row was added successfully. </returns>
        bool AddRow(IFakeRow row);
        /// <summary>
        /// Removes <see cref="IFakeRow"/> connected with given index from this <see cref="IFakeTable"/>.
        /// </summary>
        /// <param name="rowIndex"> Index from which <see cref="IFakeRow"/> should be removed. </param>
        void RemoveRow(int rowIndex);
        /// <summary>
        /// Returns <see cref="IFakeRow"/> from given Row index if Row exists; otherwise <see cref="System.ArgumentException"/> will be thrown.
        /// </summary>
        /// <param name="rowIndex"> Index if the <see cref="IFakeRow"/>. </param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException"></exception>
        IFakeRow GetRow(int rowIndex);
    }
}