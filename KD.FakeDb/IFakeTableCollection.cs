using System.Collections.Generic;

namespace KD.FakeDb
{
    /// <summary>
    /// Describes a collection of <see cref="IFakeTable"/>'s inside <see cref="IFakeDatabase"/>.
    /// </summary>
    public interface IFakeTableCollection : ICollection<IFakeTable>
    {
        /// <summary>
        /// Returns <see cref="IFakeTable"/> from given Table name if Table exists; otherwise <see cref="System.ArgumentException"/> will be thrown.
        /// </summary>
        /// <param name="tableName"> Name of the <see cref="IFakeTable"/>. </param>
        /// <returns> Returns <see cref="IFakeTable"/> connected with given name. </returns>
        /// <exception cref="System.ArgumentException"></exception>
        IFakeTable this[string tableName] { get; }

        /// <summary>
        /// Returns <see cref="IFakeTable"/> at specified index from internal list if Table exists; otherwise <see cref="System.ArgumentException"/> will be thrown.
        /// </summary>
        /// <param name="tableIndex"> Index of the <see cref="IFakeTable"/>. </param>
        /// <returns> Returns <see cref="IFakeTable"/> at specified index. </returns>
        /// <exception cref="System.ArgumentException"></exception>
        IFakeTable this[int tableIndex] { get; }

        /// <summary>
        /// Returns <see cref="IFakeDatabase"/> to which this collection belongs.
        /// </summary>
        IFakeDatabase Database { get; }

        /// <summary>
        /// Adds new <see cref="IFakeTable"/> to this <see cref="IFakeDatabase"/> with given name.
        /// </summary>
        /// <param name="tableName"> New Table name. </param>
        /// <returns> Returns newly created <see cref="IFakeTable"/>. </returns>
        /// <exception cref="System.ArgumentException"></exception>
        IFakeTable AddTable(string tableName);
        /// <summary>
        /// Removes <see cref="IFakeTable"/> connected with given name from this <see cref="IFakeDatabase"/>.
        /// </summary>
        /// <param name="tableName"> Name of the Table which should be removed. </param>
        /// <exception cref="System.ArgumentException"></exception>
        void RemoveTable(string tableName);
        /// <summary>
        /// Returns <see cref="IFakeTable"/> from given Table name if Table exists; otherwise <see cref="System.ArgumentException"/> will be thrown.
        /// </summary>
        /// <param name="tableName"> Name of the <see cref="IFakeTable"/>. </param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException"></exception>
        IFakeTable GetTable(string tableName);
    }
}