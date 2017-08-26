using System;
using System.Collections.Generic;
using System.Linq;

namespace KD.FakeDb.Linq
{
    /// <summary>
    /// Class which should simulate WHERE method.
    /// "Where" Methods can be use on <see cref="IFakeTable"/>.
    /// </summary>
    public static partial class FakeDatabaseLinq
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
            if (condition == null)
            {
                throw new ArgumentNullException(nameof(condition), "You must specify the condition which will be use to get elements.");
            }

            IList<TFakeRow> results = new List<TFakeRow>();

            foreach (var row in source.RowCollection.ToList())
            {
                if (condition.Invoke(((TFakeRow)row)))
                {
                    results.Add((TFakeRow)row);
                }
            }

            return results;
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
            if (condition == null)
            {
                throw new ArgumentNullException(nameof(condition), "You must specify the condition which will be use to get elements.");
            }

            IDictionary<TFakeTable, ISet<TFakeRow>> results = new Dictionary<TFakeTable, ISet<TFakeRow>>();

            foreach (var table in source.TableCollection)
            {
                foreach (var row in table.RowCollection)
                {
                    if (condition.Invoke((TFakeRow)row))
                    {
                        // Add row to results
                        ISet<TFakeRow> set = null;
                        try
                        {
                            // Try to get Set.
                            set = results[(TFakeTable)table];
                        }
                        catch (KeyNotFoundException ex) // Catch if Set is not in Dictionary and add new.
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
        /// Returns <see cref="IDictionary{TKey, TValue}"/> where Key is a <see cref="IFakeTable"/> 
        /// and Value is a <see cref="ISet{T}"/> of <see cref="IFakeRow"/>s which meets the given condition.
        /// </summary>
        /// <typeparam name="TFakeDatabase"> Type of input <see cref="IFakeDatabase"/>. </typeparam>
        /// <typeparam name="TFakeTable"> Type of searched <see cref="IFakeTable"/>s. </typeparam>
        /// <typeparam name="TFakeRow"> Type of result <see cref="IFakeRow"/>s. </typeparam>
        /// <param name="source"> Input <see cref="IFakeDatabase"/> which will be searched. </param>
        /// <param name="conditions"> Conditions which will be used to determine result. </param>
        /// <returns></returns>
        public static IDictionary<TFakeTable, ISet<TFakeRow>> Where<TFakeDatabase, TFakeTable, TFakeRow>(this TFakeDatabase source, ICollection<Func<TFakeRow, bool>> conditions)
            where TFakeDatabase : IFakeDatabase
            where TFakeTable : IFakeTable
            where TFakeRow : IFakeRow
        {
            if (conditions == null && conditions.Count == 0)
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
                        catch (KeyNotFoundException ex) // Catch if Set is not in Dictionary and add new.
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
    }
}