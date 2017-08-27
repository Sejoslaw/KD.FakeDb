using System;
using System.Collections.Generic;
using System.Linq;

namespace KD.FakeDb.Linq
{
    /// <summary>
    /// Defines Linq methods for <see cref="IFakeDatabase"/>.
    /// </summary>
    public static class FakeDatabaseLinq
    {
        /// <summary>
        /// Returns <see cref="IDictionary{TKey, TValue}"/> where Key is a <see cref="IFakeTable"/> 
        /// and Value is a <see cref="ISet{T}"/> of <see cref="IFakeRow"/>s which meets the given condition.
        /// </summary>
        /// <param name="source"> Input <see cref="IFakeDatabase"/> which will be searched. </param>
        /// <param name="condition"> Condition which will be used to determine result. </param>
        /// <returns></returns>
        public static IDictionary<IFakeTable, HashSet<IFakeRow>> Where(this IFakeDatabase source, Func<IFakeRow, bool> condition)
        {
            return Where(source, new List<Func<IFakeRow, bool>>() { condition });
        }

        /// <summary>
        /// Returns <see cref="IDictionary{TKey, TValue}"/> where Key is a <see cref="IFakeTable"/> 
        /// and Value is a <see cref="ISet{T}"/> of <see cref="IFakeRow"/>s which meets the given condition.
        /// </summary>
        /// <param name="source"> Input <see cref="IFakeDatabase"/> which will be searched. </param>
        /// <param name="conditions"> Conditions which will be used to determine result. </param>
        /// <returns></returns>
        public static IDictionary<IFakeTable, HashSet<IFakeRow>> Where(this IFakeDatabase source, IEnumerable<Func<IFakeRow, bool>> conditions)
        {
            if (conditions == null && !conditions.Any())
            {
                throw new ArgumentNullException(nameof(conditions), "You must specify at least one condition which will be use to get elements.");
            }

            IDictionary<IFakeTable, HashSet<IFakeRow>> results = new Dictionary<IFakeTable, HashSet<IFakeRow>>();

            foreach (var table in source.TableCollection)
            {
                foreach (var row in table.RowCollection)
                {
                    bool shouldBeAdded = true;
                    foreach (var condition in conditions)
                    {
                        if (!condition(row))
                        {
                            shouldBeAdded = false;
                            break;
                        }
                    }

                    if (shouldBeAdded)
                    {
                        // Add row to results
                        HashSet<IFakeRow> set = null;
                        try
                        {
                            // Try to get Set.
                            set = results[table];
                        }
                        catch (KeyNotFoundException) // Catch if Set is not in Dictionary and add new.
                        {
                            set = new HashSet<IFakeRow>();
                            results[table] = set;
                        }
                        set.Add(row);
                    }
                }
            }

            return results;
        }
    }
}