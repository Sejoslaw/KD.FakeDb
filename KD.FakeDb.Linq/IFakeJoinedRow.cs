using System.Collections.Generic;

namespace KD.FakeDb.Linq
{
    /// <summary>
    /// Simulates multiple joined <see cref="IFakeRow"/>s.
    /// It is a Read-Only object.
    /// Any changes will NOT affect actual <see cref="IFakeDatabase"/>'s Rows.
    /// </summary>
    public interface IFakeJoinedRow : IEnumerable<KeyValuePair<string, object>> // Key -> Column Name, Value -> value in that Column
    {
        /// <summary>
        /// Returns the value in this <see cref="IFakeJoinedRow"/> from specified <see cref="IFakeColumn"/>'s Name. If column don't exists <see cref="System.ArgumentException"/> wil be thrown.
        /// </summary>
        /// <param name="columnName"> Name of the <see cref="IFakeColumn"/> from which to return value for this <see cref="IFakeJoinedRow"/>. </param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException"></exception>
        object this[string columnName] { get; }

        /// <summary>
        /// Returns <see cref="IDictionary{TKey, TValue}"/> which contains all values from joined rows.
        /// </summary>
        IDictionary<string, object> Values { get; }

        /// <summary>
        /// Returns true if given <see cref="IFakeRow"/> can be added to this <see cref="IFakeJoinedRow"/>, 
        /// if given column name is equal with the same column already in this <see cref="IFakeJoinedRow"/>.
        /// </summary>
        /// <param name="row"> <see cref="IFakeRow"/> which should be added to this <see cref="IFakeJoinedRow"/>. </param>
        /// <param name="columnName"> Column name by which two rows are connected. </param>
        /// <returns></returns>
        bool CanJoinRow(IFakeRow row, string columnName);

        /// <summary>
        /// Returns true if given <see cref="IFakeJoinedRow"/> can be added to this <see cref="IFakeJoinedRow"/>, 
        /// if given column name is equal with the same column already in this <see cref="IFakeJoinedRow"/>.
        /// </summary>
        /// <param name="row"> <see cref="IFakeJoinedRow"/> which should be added to this <see cref="IFakeJoinedRow"/>. </param>
        /// <param name="columnName"> Column name by which two rows are connected. </param>
        /// <returns></returns>
        bool CanJoinRow(IFakeJoinedRow row, string columnName);

        /// <summary>
        /// Returns this <see cref="IFakeJoinedRow"/>.
        /// Adds only values from columns which are not connected.
        /// In other words, if there is already registered Column it will not be added, even if there is a different value.
        /// </summary>
        /// <param name="row"> <see cref="IFakeRow"/> which should be added to this <see cref="IFakeJoinedRow"/>. </param>
        /// <returns></returns>
        void JoinRow(IFakeRow row);

        /// <summary>
        /// Add <see cref="IFakeJoinedRow"/> to this <see cref="IFakeJoinedRow"/>.
        /// </summary>
        /// <param name="row"> <see cref="IFakeJoinedRow"/> which should be added to this <see cref="IFakeJoinedRow"/>. </param>
        void JoinRow(IFakeJoinedRow row);

        /// <summary>
        /// Adds given <see cref="IFakeJoinedRow"/> to this one and returns new <see cref="IFakeJoinedRow"/>.
        /// </summary>
        /// <param name="row"> <see cref="IFakeJoinedRow"/> which should be added. </param>
        /// <returns> Returns new <see cref="IFakeJoinedRow"/> which was formed by combining this and given <see cref="IFakeJoinedRow"/>. </returns>
        IFakeJoinedRow JoinToNew(IFakeJoinedRow row);
    }
}