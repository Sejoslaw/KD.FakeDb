using System;
using System.Collections.Generic;
using System.Linq;

namespace KD.FakeDb.Linq
{
    /// <summary>
    /// Defines Linq methods for <see cref="IFakeTable"/>.
    /// </summary>
    public static partial class FakeTableLinq
    {
        /// <summary>
        /// Returns <see cref="IList{T}"/> of all <see cref="IFakeRow"/>s which meets the given condition.
        /// </summary>
        /// <typeparam name="TFakeTable"> Type of input <see cref="IFakeTable"/>. </typeparam>
        /// <typeparam name="TFakeRow"> Type of result <see cref="IFakeRow"/>. </typeparam>
        /// <param name="source"> Input <see cref="IFakeTable"/>. </param>
        /// <param name="condition"> Input condition. </param>
        /// <returns></returns>
        public static IList<TFakeRow> Where<TFakeTable, TFakeRow>(this TFakeTable source, Func<TFakeRow, bool> condition)
            where TFakeTable : IFakeTable
            where TFakeRow : IFakeRow
        {
            return Where(source, new List<Func<TFakeRow, bool>>() { condition });
        }

        /// <summary>
        /// Returns <see cref="IList{T}"/> of all <see cref="IFakeRow"/>s which meets the given conditions.
        /// </summary>
        /// <typeparam name="TFakeTable"> Type of input <see cref="IFakeTable"/>. </typeparam>
        /// <typeparam name="TFakeRow"> Type of result <see cref="IFakeRow"/>. </typeparam>
        /// <param name="source"> Input <see cref="IFakeTable"/>. </param>
        /// <param name="conditions"> Input conditions. </param>
        /// <returns></returns>
        public static IList<TFakeRow> Where<TFakeTable, TFakeRow>(this TFakeTable source, ICollection<Func<TFakeRow, bool>> conditions)
            where TFakeTable : IFakeTable
            where TFakeRow : IFakeRow
        {
            if (conditions == null && conditions.Count <= 0)
            {
                throw new ArgumentNullException(nameof(conditions), "You must specify at least one condition which will be use to get elements.");
            }

            IList<TFakeRow> results = new List<TFakeRow>();

            foreach (var row in source.RowCollection.ToList())
            {
                bool shouldBeAdded = true;
                foreach (var condition in conditions)
                {
                    if (!condition.Invoke((TFakeRow)row))
                    {
                        shouldBeAdded = false;
                        break;
                    }
                }

                if (shouldBeAdded)
                {
                    results.Add((TFakeRow)row);
                }
            }

            return results;
        }

        /// <summary>
        /// Returns <see cref="List{T}"/> which contains all <see cref="IFakeRow"/>s from <see cref="IFakeTable"/>.
        /// </summary>
        /// <typeparam name="TFakeTable"> Type of input <see cref="IFakeTable"/>. </typeparam>
        /// <typeparam name="TFakeRow"> Type of result <see cref="IFakeRow"/>s. </typeparam>
        /// <param name="source"> Input <see cref="IFakeTable"/>. </param>
        /// <returns></returns>
        public static List<TFakeRow> ToRowsList<TFakeTable, TFakeRow>(this TFakeTable source)
            where TFakeTable : IFakeTable
            where TFakeRow : IFakeRow
        {
            List<TFakeRow> rows = new List<TFakeRow>();
            source.RowCollection.ToList().ForEach(row => rows.Add((TFakeRow)row));
            return rows;
        }

        /// <summary>
        /// Returns <see cref="List{T}"/> which contains all <see cref="IFakeColumn"/>s from <see cref="IFakeTable"/>.
        /// </summary>
        /// <typeparam name="TFakeTable"> Type of input <see cref="IFakeTable"/>. </typeparam>
        /// <typeparam name="TFakeColumn"> Type of result <see cref="IFakeColumn"/>. </typeparam>
        /// <param name="source"> Input <see cref="IFakeTable"/>. </param>
        /// <returns></returns>
        public static List<TFakeColumn> ToColumnsList<TFakeTable, TFakeColumn>(this TFakeTable source)
            where TFakeTable : IFakeTable
            where TFakeColumn : IFakeColumn
        {
            List<TFakeColumn> columns = new List<TFakeColumn>();
            source.ColumnCollection.ToList().ForEach(column => columns.Add((TFakeColumn)column));
            return columns;
        }

        /// <summary>
        /// Performs the specified action on each <see cref="IFakeColumn"/> from this <see cref="IFakeTable"/>.
        /// </summary>
        /// <typeparam name="TFakeTable"> Type of <see cref="IFakeTable"/>. </typeparam>
        /// <typeparam name="TFakeColumn"> Type of <see cref="IFakeColumn"/>. </typeparam>
        /// <param name="source"> This <see cref="IFakeTable"/>. </param>
        /// <param name="action"> <see cref="Action"/> which will be performed on each <see cref="IFakeColumn"/> in this <see cref="IFakeTable"/>. </param>
        public static void ForEachColumn<TFakeTable, TFakeColumn>(this TFakeTable source, Action<TFakeColumn> action)
            where TFakeTable : IFakeTable
            where TFakeColumn : IFakeColumn
        {
            for (int i = 0; i < source.ColumnCollection.Count; ++i)
            {
                action((TFakeColumn)source.ColumnCollection[i]);
            }
        }

        /// <summary>
        /// Performs the specified action on each <see cref="IFakeRow"/> from this <see cref="IFakeTable"/>.
        /// </summary>
        /// <typeparam name="TFakeTable"> Type of <see cref="IFakeTable"/>. </typeparam>
        /// <typeparam name="TFakeRow"> Type of <see cref="IFakeRow"/>. </typeparam>
        /// <param name="source"> This <see cref="IFakeTable"/>. </param>
        /// <param name="action"> <see cref="Action"/> which will be performed on each <see cref="IFakeRow"/> in this <see cref="IFakeTable"/>. </param>
        public static void ForEachRow<TFakeTable, TFakeRow>(this TFakeTable source, Action<TFakeRow> action)
            where TFakeTable : IFakeTable
            where TFakeRow : IFakeRow
        {
            for (int i = 0; i < source.RowCollection.Count; ++i)
            {
                action((TFakeRow)source.RowCollection[i]);
            }
        }
    }
}