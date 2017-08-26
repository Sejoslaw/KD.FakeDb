﻿using System;
using System.Collections.Generic;

namespace KD.FakeDb.Linq
{
    /// <summary>
    /// Describes Linq methods for <see cref="IFakeRow"/>.
    /// </summary>
    public static class FakeRowLinq
    {
        /// <summary>
        /// Performs the specified action on each element of this <see cref="IFakeRow"/>.
        /// </summary>
        /// <typeparam name="TFakeRow"> Type of input <see cref="IFakeRow"/>. </typeparam>
        /// <param name="row"> <see cref="IFakeRow"/> on which specified <see cref="Action"/> will be performed. </param>
        /// <param name="action"> <see cref="Action"/> delegate to perform on each <see cref="IFakeRow"/>'s element. </param>
        public static void ForEachInRow<TFakeRow>(this TFakeRow row, Action<KeyValuePair<string, object>> action)
            where TFakeRow : IFakeRow
        {
            for (int i = 0; i < row.Count; ++i)
            {
                var column = row.Table.ColumnCollection[i];
                action(new KeyValuePair<string, object>(column.Name, row[i]));
            }
        }

        /// <summary>
        /// Converts <see cref="IFakeRow"/> to <see cref="IFakeJoinedRow"/>.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IFakeJoinedRow ToJoinedRow(this IFakeRow source)
        {
            return new FakeJoinedRow(source);
        }
    }
}