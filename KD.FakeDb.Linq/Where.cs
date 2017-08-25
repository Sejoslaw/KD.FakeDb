using System;
using System.Collections.Generic;
using System.Linq;

namespace KD.FakeDb.Linq
{
    /// <summary>
    /// Class which should simulate WHERE method.
    /// Where Methods can be use on <see cref="IFakeTable"/>.
    /// </summary>
    public static partial class FakeDatabaseLinq
    {
        /// <summary>
        /// Returns <see cref="IEnumerable{T}"/> of all <see cref="IFakeRow"/>s which meets the given condition.
        /// </summary>
        /// <typeparam name="TSource"> Type of input <see cref="IFakeTable"/>. </typeparam>
        /// <typeparam name="TResult"> Type of result <see cref="IFakeRow"/>. </typeparam>
        /// <param name="source"> Input <see cref="IFakeTable"/>. </param>
        /// <param name="condition"> Input condition. </param>
        /// <returns></returns>
        public static IEnumerable<TResult> Where<TSource, TResult>(this TSource source, Func<TResult, bool> condition)
            where TSource : IFakeTable
            where TResult : IFakeRow
        {
            if (condition == null)
            {
                throw new ArgumentNullException(nameof(condition), "You must specify the condition which will be use to get elements.");
            }

            List<TResult> results = new List<TResult>();

            foreach (var row in source.RowCollection.ToList())
            {
                if (condition.Invoke(((TResult)row)))
                {
                    results.Add((TResult)row);
                }
            }

            return results;
        }

        /// <summary>
        /// Returns <see cref="IEnumerable{T}"/> of all <see cref="IFakeRow"/>s which meets the given conditions.
        /// </summary>
        /// <typeparam name="TSource"> Type of input <see cref="IFakeTable"/>. </typeparam>
        /// <typeparam name="TResult"> Type of result <see cref="IFakeRow"/>. </typeparam>
        /// <param name="source"> Input <see cref="IFakeTable"/>. </param>
        /// <param name="conditions"> Input conditions. </param>
        /// <returns></returns>
        public static IEnumerable<TResult> Where<TSource, TResult>(this TSource source, List<Func<TResult, bool>> conditions)
            where TSource : IFakeTable
            where TResult : IFakeRow
        {
            if (conditions == null && conditions.Count <= 0)
            {
                throw new ArgumentNullException(nameof(conditions), "You must specify at least one condition which will be use to get elements.");
            }

            List<TResult> results = new List<TResult>();

            foreach (var row in source.RowCollection.ToList())
            {
                bool shouldBeAdded = true;
                foreach (var condition in conditions)
                {
                    if (!condition.Invoke((TResult)row))
                    {
                        shouldBeAdded = false;
                        break;
                    }
                }

                if (shouldBeAdded)
                {
                    results.Add((TResult)row);
                }
            }

            return results;
        }
    }
}