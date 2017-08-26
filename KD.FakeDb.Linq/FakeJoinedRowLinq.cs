using System;
using System.Collections.Generic;
using System.Linq;

namespace KD.FakeDb.Linq
{
    /// <summary>
    /// Describes Linq methods for <see cref="IFakeJoinedRow"/>.
    /// </summary>
    public static class FakeJoinedRowLinq
    {
        /// <summary>
        /// Performs the specified action on each element of this <see cref="IFakeJoinedRow"/>.
        /// </summary>
        /// <typeparam name="TFakeJoinedRow"> Type of input <see cref="IFakeJoinedRow"/>. </typeparam>
        /// <param name="source"> <see cref="IFakeJoinedRow"/> on which specified <see cref="Action"/> will be performed. </param>
        /// <param name="action"> <see cref="Action"/> delegate to perform on each <see cref="IFakeJoinedRow"/>'s element. </param>
        public static void ForEachInJoinedRow<TFakeJoinedRow>(this TFakeJoinedRow source, Action<KeyValuePair<string, object>> action)
            where TFakeJoinedRow : IFakeJoinedRow
        {
            var sourceList = source.Values.ToList();
            for (int i = 0; i < sourceList.Count; ++i)
            {
                action(sourceList[i]);
            }
        }

        /// <summary>
        /// Join current <see cref="IFakeJoinedRow"/> with given <see cref="IFakeJoinedRow"/> using given Column Name.
        /// </summary>
        /// <typeparam name="TFakeJoinedRow"> Type of <see cref="IFakeJoinedRow"/>. </typeparam>
        /// <param name="source"> This <see cref="IFakeJoinedRow"/>. </param>
        /// <param name="row"> <see cref="IFakeJoinedRow"/> which should be added to this Row. </param>
        /// <returns></returns>
        public static TFakeJoinedRow JoinRow<TFakeJoinedRow>(this TFakeJoinedRow source, TFakeJoinedRow row)
            where TFakeJoinedRow : IFakeJoinedRow
        {
            return (TFakeJoinedRow)source.JoinToNew(row);
        }

        /// <summary>
        /// Returns <see cref="IEnumerable{T}"/> which contains <see cref="IFakeJoinedRow"/>s 
        /// which were made by adding <see cref="IFakeRow"/> to this <see cref="IFakeJoinedRow"/>.
        /// This is in form of <see cref="IEnumerable{T}"/> because multiple <see cref="IFakeRow"/>s could have value in specified <see cref="IFakeColumn"/> equal with this <see cref="IFakeJoinedRow"/>.
        /// </summary>
        /// <typeparam name="TFakeJoinedRow"></typeparam>
        /// <typeparam name="TFakeTable"></typeparam>
        /// <typeparam name="TFakeRow"></typeparam>
        /// <param name="source"></param>
        /// <param name="table"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public static IEnumerable<TFakeJoinedRow> JoinRow<TFakeJoinedRow, TFakeTable, TFakeRow>(this TFakeJoinedRow source, TFakeTable table, string columnName)
            where TFakeJoinedRow : IFakeJoinedRow
            where TFakeTable : IFakeTable
            where TFakeRow : IFakeRow
        {
            List<TFakeJoinedRow> list = new List<TFakeJoinedRow>();

            table.ForEachRow<TFakeTable, TFakeRow>(row =>
            {
                if (source.CanJoinRow(row, columnName))
                {
                    list.Add((TFakeJoinedRow)source.JoinToNew(row.ToJoinedRow()));
                }
            });

            return list;
        }

        /// <summary>
        /// Returns <see cref="IDictionary{TKey, TValue}"/> which contains <see cref="KeyValuePair{TKey, TValue}"/>s 
        /// where Key is checked <see cref="IFakeTable"/> and
        /// Value is a <see cref="IEnumerable{T}"/> which contains <see cref="IFakeJoinedRow"/>s 
        /// which were made by adding <see cref="IFakeRow"/> to this <see cref="IFakeJoinedRow"/> by given Column Name.
        /// This is in form of <see cref="IEnumerable{T}"/> because multiple <see cref="IFakeRow"/>s could have value in specified <see cref="IFakeColumn"/> equal with this <see cref="IFakeJoinedRow"/>.
        /// </summary>
        /// <typeparam name="TFakeJoinedRow"></typeparam>
        /// <typeparam name="TFakeTable"></typeparam>
        /// <typeparam name="TFakeRow"></typeparam>
        /// <param name="source"></param>
        /// <param name="tables"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public static IDictionary<TFakeTable, IEnumerable<TFakeJoinedRow>> JoinRow<TFakeJoinedRow, TFakeTable, TFakeRow>(this TFakeJoinedRow source, IEnumerable<TFakeTable> tables, string columnName)
            where TFakeJoinedRow : IFakeJoinedRow
            where TFakeTable : IFakeTable
            where TFakeRow : IFakeRow
        {
            IDictionary<TFakeTable, IEnumerable<TFakeJoinedRow>> result = new Dictionary<TFakeTable, IEnumerable<TFakeJoinedRow>>();

            tables.ForEach(table =>
            {
                result.Add(table, JoinRow<TFakeJoinedRow, TFakeTable, TFakeRow>(source, table, columnName));
            });

            return result;
        }
    }
}