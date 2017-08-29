using System;

namespace KD.FakeDb
{
    /// <summary>
    /// Describes single Table in <see cref="IFakeDatabase"/>.
    /// </summary>
    public interface IFakeTable
    {
        /// <summary>
        /// Returns <see cref="IFakeColumn"/> from given Column name if Column exists; otherwise <see cref="System.ArgumentException"/> will be thrown.
        /// </summary>
        /// <param name="columnName"> Name of the <see cref="IFakeColumn"/> </param>
        /// <returns> Returns <see cref="IFakeColumn"/> connected with given column name. </returns>
        /// <exception cref="System.ArgumentException"></exception>
        IFakeColumn this[string columnName] { get; }

        /// <summary>
        /// Returns <see cref="IFakeRow"/> from given Row index if Row exists; otherwise <see cref="System.ArgumentException"/> will be thrown.
        /// </summary>
        /// <param name="rowIndex"> Index of the <see cref="IFakeRow"/>. </param>
        /// <returns> Returns single <see cref="IFakeRow"/> from this <see cref="IFakeTable"/>. </returns>
        IFakeRow this[int rowIndex] { get; }

        /// <summary>
        /// Returns the Name of this <see cref="IFakeTable"/>.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Returns a <see cref="IFakeColumnCollection"/> of Columns which this <see cref="IFakeTable"/> contains.
        /// </summary>
        IFakeColumnCollection ColumnCollection { get; }

        /// <summary>
        /// Returns a <see cref="IFakeRowCollection"/> of Rows which this <see cref="IFakeTable"/> contains.
        /// </summary>
        IFakeRowCollection RowCollection { get; }

        /// <summary>
        /// Returns <see cref="IFakeDatabase"/> to which this Table belongs.
        /// </summary>
        IFakeDatabase Database { get; }

        /// <summary>
        /// Adds new <see cref="IFakeColumn"/> to this Table.
        /// </summary>
        /// <param name="column"></param>
        void AddColumn(IFakeColumn column);
        /// <summary>
        /// Adds new <see cref="IFakeColumn"/> to this <see cref="IFakeTable"/> with given name.
        /// </summary>
        /// <param name="columnName"> New Column name. </param>
        /// <param name="columnObjectType"> Type of the objects which this Column can hold. </param>
        /// <returns> Return newly created <see cref="IFakeColumn"/>. </returns>
        /// <exception cref="System.ArgumentException"></exception>
        IFakeColumn AddColumn(string columnName, Type columnObjectType);
        /// <summary>
        /// Removes <see cref="IFakeColumn"/> connected with given name from this <see cref="IFakeTable"/>.
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