﻿using System.Collections.Generic;

namespace KD.FakeDb
{
    /// <summary>
    /// Describes the main functionality of Fake Database.
    /// This is the Core of Fake Database.
    /// </summary>
    public interface IFakeDatabase : ICollection<IFakeTable>
    {
        /// <summary>
        /// Returns <see cref="IFakeTable"/> from given Table name if Table exists; otherwise <see cref="System.ArgumentException"/> will be thrown.
        /// </summary>
        /// <param name="tableName"> Name of the <see cref="IFakeTable"/>. </param>
        /// <returns> Returns <see cref="IFakeTable"/> connected with given name. </returns>
        /// <exception cref="System.ArgumentException"></exception>
        IFakeTable this[string tableName] { get; set; }

        /// <summary>
        /// Returns the Name of Database.
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// Returns a collection of Tables which this <see cref="IFakeDatabase"/> contains.
        /// </summary>
        IEnumerable<IFakeTable> Tables { get; }

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