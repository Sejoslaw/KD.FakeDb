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
        /// <typeparam name="TFakeDatabase"> Type of input <see cref="IFakeDatabase"/>. </typeparam>
        /// <typeparam name="TFakeTable"> Type of searched <see cref="IFakeTable"/>s. </typeparam>
        /// <typeparam name="TFakeRow"> Type of result <see cref="IFakeRow"/>s. </typeparam>
        /// <param name="source"> Input <see cref="IFakeDatabase"/> which will be searched. </param>
        /// <param name="condition"> Condition which will be used to determine result. </param>
        /// <returns></returns>
        public static IDictionary<TFakeTable, ISet<TFakeRow>> Where<TFakeDatabase, TFakeTable, TFakeRow>(this TFakeDatabase source, Func<TFakeRow, bool> condition)
            where TFakeDatabase : IFakeDatabase
            where TFakeTable : IFakeTable
            where TFakeRow : IFakeRow
        {
            return Where<TFakeDatabase, TFakeTable, TFakeRow>(source, new List<Func<TFakeRow, bool>>() { condition });
        }

        /// <summary>
        /// Returns <see cref="IDictionary{TKey, TValue}"/> where Key is a <see cref="IFakeTable"/> 
        /// and Value is a <see cref="ISet{T}"/> of <see cref="IFakeRow"/>s which meets the given condition.
        /// </summary>
        /// <typeparam name="TFakeDatabase"> Type of input <see cref="IFakeDatabase"/>. </typeparam>
        /// <typeparam name="TFakeTable"> Type of searched <see cref="IFakeTable"/>s. </typeparam>
        /// <typeparam name="TFakeRow"> Type of result <see cref="IFakeRow"/>s. </typeparam>
        /// <param name="source"> Input <see cref="IFakeDatabase"/> which will be searched. </param>
        /// <param name="conditions"> Conditions which will be used to determine result. </param>
        /// <returns></returns>
        public static IDictionary<TFakeTable, ISet<TFakeRow>> Where<TFakeDatabase, TFakeTable, TFakeRow>(this TFakeDatabase source, IEnumerable<Func<TFakeRow, bool>> conditions)
            where TFakeDatabase : IFakeDatabase
            where TFakeTable : IFakeTable
            where TFakeRow : IFakeRow
        {
            if (conditions == null && !conditions.Any())
            {
                throw new ArgumentNullException(nameof(conditions), "You must specify at least one condition which will be use to get elements.");
            }

            IDictionary<TFakeTable, ISet<TFakeRow>> results = new Dictionary<TFakeTable, ISet<TFakeRow>>();

            foreach (var table in source.TableCollection)
            {
                foreach (var row in table.RowCollection)
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
                        // Add row to results
                        ISet<TFakeRow> set = null;
                        try
                        {
                            // Try to get Set.
                            set = results[(TFakeTable)table];
                        }
                        catch (KeyNotFoundException) // Catch if Set is not in Dictionary and add new.
                        {
                            set = new HashSet<TFakeRow>();
                            results[(TFakeTable)table] = set;
                        }
                        set.Add((TFakeRow)row);
                    }
                }
            }

            return results;
        }

        /// <summary>
        /// Returns <see cref="List{T}"/> which contains all <see cref="IFakeTable"/>s from <see cref="IFakeDatabase"/>.
        /// </summary>
        /// <typeparam name="TFakeDatabase"> Type of input <see cref="IFakeDatabase"/>. </typeparam>
        /// <typeparam name="TFakeTable"> Type of result <see cref="IFakeTable"/>s. </typeparam>
        /// <param name="source"> Input <see cref="IFakeDatabase"/>. </param>
        /// <returns></returns>
        public static List<TFakeTable> ToList<TFakeDatabase, TFakeTable>(this TFakeDatabase source)
            where TFakeDatabase : IFakeDatabase
            where TFakeTable : IFakeTable
        {
            List<TFakeTable> tables = new List<TFakeTable>();
            source.TableCollection.ToList().ForEach(table => tables.Add((TFakeTable)table));
            return tables;
        }

        /// <summary>
        /// Performs the specified action on each <see cref="IFakeTable"/> from this <see cref="IFakeDatabase"/>.
        /// </summary>
        /// <typeparam name="TFakeDatabase"> Type of <see cref="IFakeDatabase"/>. </typeparam>
        /// <typeparam name="TFakeTable"> Type of <see cref="IFakeTable"/>. </typeparam>
        /// <param name="source"> This <see cref="IFakeDatabase"/>. </param>
        /// <param name="action"> <see cref="Action"/> delegate to perform on each <see cref="IFakeTable"/> from this <see cref="IFakeDatabase"/>. </param>
        public static void ForEachTable<TFakeDatabase, TFakeTable>(this TFakeDatabase source, Action<TFakeTable> action)
            where TFakeDatabase : IFakeDatabase
            where TFakeTable : IFakeTable
        {
            for (int i = 0; i < source.Count; ++i)
            {
                action((TFakeTable)source.TableCollection[i]);
            }
        }
    }
}