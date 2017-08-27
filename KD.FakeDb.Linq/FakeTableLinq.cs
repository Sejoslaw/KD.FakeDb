using System;
using System.Collections.Generic;
using System.Linq;

namespace KD.FakeDb.Linq
{
    /// <summary>
    /// Defines Linq methods for <see cref="IFakeTable"/>.
    /// </summary>
    public static class FakeTableLinq
    {
        /// <summary>
        /// Returns <see cref="List{T}"/> of all <see cref="IFakeRow"/>s which meets the given condition.
        /// </summary>
        /// <param name="source"> Input <see cref="IFakeTable"/>. </param>
        /// <param name="condition"> Input condition. </param>
        /// <returns></returns>
        public static List<IFakeRow> Where(this IFakeTable source, Func<IFakeRow, bool> condition)
        {
            return Where(source, new List<Func<IFakeRow, bool>>() { condition });
        }

        /// <summary>
        /// Returns <see cref="List{T}"/> of all <see cref="IFakeRow"/>s which meets the given conditions.
        /// </summary>
        /// <param name="source"> Input <see cref="IFakeTable"/>. </param>
        /// <param name="conditions"> Input conditions. </param>
        /// <returns></returns>
        public static List<IFakeRow> Where(this IFakeTable source, IEnumerable<Func<IFakeRow, bool>> conditions)
        {
            if (conditions == null && !conditions.Any())
            {
                throw new ArgumentNullException(nameof(conditions), "You must specify at least one condition which will be use to get elements.");
            }

            List<IFakeRow> results = new List<IFakeRow>();

            foreach (var row in source.RowCollection.ToList())
            {
                bool shouldBeAdded = true;
                foreach (var condition in conditions)
                {
                    if (!condition.Invoke(row))
                    {
                        shouldBeAdded = false;
                        break;
                    }
                }

                if (shouldBeAdded)
                {
                    results.Add(row);
                }
            }

            return results;
        }
    }
}