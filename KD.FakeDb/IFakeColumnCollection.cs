using System;
using System.Collections.Generic;

namespace KD.FakeDb
{
    /// <summary>
    /// Describes a collection of <see cref="IFakeColumn"/>'s inside <see cref="IFakeTable"/>.
    /// </summary>
    public interface IFakeColumnCollection : IEnumerable<IFakeColumn>
    {
        /// <summary>
        /// Returns <see cref="IFakeColumn"/> from given Column name if Column exists; otherwise <see cref="System.ArgumentException"/> will be thrown.
        /// </summary>
        /// <param name="columnName"> Name of the <see cref="IFakeColumn"/> </param>
        /// <returns> Returns <see cref="IFakeColumn"/> connected with given column name. </returns>
        /// <exception cref="System.ArgumentException"></exception>
        IFakeColumn this[string columnName] { get; }

        /// <summary>
        /// Returns <see cref="IFakeColumn"/> from given Column index if Column exists; otherwise <see cref="System.ArgumentException"/> will be thrown.
        /// </summary>
        /// <param name="columnIndex"> Index of the <see cref="IFakeColumn"/>. </param>
        /// <returns> Returns <see cref="IFakeColumn"/> connected with given column name. </returns>
        /// <exception cref="System.ArgumentException"></exception>
        IFakeColumn this[int columnIndex] { get; }

        /// <summary>
        /// Returns the number of Columns in this collection.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Returns <see cref="IFakeTable"/> to which this collection belongs.
        /// </summary>
        IFakeTable Table { get; }

        /// <summary>
        /// Adds new <see cref="IFakeColumn"/> to this <see cref="IFakeTable"/> with given name.
        /// </summary>
        /// <param name="columnName"> New Column name. </param>
        /// <param name="columnObjectType"> Type of the objects which this Column can hold. </param>
        /// <returns> Return newly created <see cref="IFakeColumn"/>. </returns>
        /// <exception cref="System.ArgumentException"></exception>
        IFakeColumn AddColumn(string columnName, Type columnObjectType);
        /// <summary>
        /// Removes <see cref="IFakeColumn"/> connected with given name from this <see cref="IFakeColumnCollection"/>.
        /// </summary>
        /// <param name="columnName"> Name of the Column which should be removed. </param>
        /// <exception cref="System.ArgumentException"></exception>
        void RemoveColumn(string columnName);
        /// <summary>
        /// Returns <see cref="IFakeColumn"/> from given Column name if Column exists; otherwise <see cref="System.ArgumentException"/> will be thrown.
        /// </summary>
        /// <param name="columnName"> Name of the <see cref="IFakeColumn"/>. </param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException"></exception>
        IFakeColumn GetColumn(string columnName);
    }
}