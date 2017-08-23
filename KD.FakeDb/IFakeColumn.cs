using System;
using System.Collections.Generic;

namespace KD.FakeDb
{
    /// <summary>
    /// Describes single Column in <see cref="IFakeTable"/>.
    /// </summary>
    public interface IFakeColumn : IEnumerable<object>
    {
        /// <summary>
        /// Returns the object on specified index in this <see cref="IFakeColumn"/>. If index is less than 0, <see cref="System.ArgumentException"/> will be thrown.
        /// </summary>
        /// <param name="index"> Column index from which value should be returned. </param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException"></exception>
        object this[int index] { get; set; }

        /// <summary>
        /// Returns the Name of this <see cref="IFakeColumn"/>.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Returns the <see cref="System.Type"/> of the objects which can be stored in this <see cref="IFakeColumn"/>.
        /// </summary>
        Type Type { get; }

        /// <summary>
        /// Returns the number of elements in this <see cref="IFakeColumn"/>.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Returns the <see cref="IFakeTable"/> which contains this <see cref="IFakeColumn"/>.
        /// </summary>
        IFakeTable Table { get; }
    }
}