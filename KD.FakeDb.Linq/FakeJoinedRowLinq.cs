using System.Collections.Generic;

namespace KD.FakeDb.Linq
{
    /// <summary>
    /// Describes Linq methods for <see cref="IFakeJoinedRow"/>.
    /// </summary>
    public static class FakeJoinedRowLinq
    {
        /// <summary>
        /// Returns <see cref="List{T}"/> which contains <see cref="IFakeJoinedRow"/>s 
        /// which were made by adding <see cref="IFakeRow"/> to this <see cref="IFakeJoinedRow"/>.
        /// This is in form of <see cref="List{T}"/> because multiple <see cref="IFakeRow"/>s could have value in specified <see cref="IFakeColumn"/> equal with this <see cref="IFakeJoinedRow"/>.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="table"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public static List<IFakeJoinedRow> JoinRow(this IFakeJoinedRow source, IFakeTable table, string columnName)
        {
            List<IFakeJoinedRow> list = new List<IFakeJoinedRow>();

            table.RowCollection.ForEach(row =>
            {
                if (source.CanJoinRow(row, columnName))
                {
                    list.Add(source.JoinToNew(row.ToJoinedRow()));
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
        /// <param name="source"></param>
        /// <param name="tables"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public static IDictionary<IFakeTable, List<IFakeJoinedRow>> JoinRow(this IFakeJoinedRow source, IEnumerable<IFakeTable> tables, string columnName)
        {
            IDictionary<IFakeTable, List<IFakeJoinedRow>> result = new Dictionary<IFakeTable, List<IFakeJoinedRow>>();

            tables.ForEach(table =>
            {
                result.Add(table, JoinRow(source, table, columnName));
            });

            return result;
        }
    }
}