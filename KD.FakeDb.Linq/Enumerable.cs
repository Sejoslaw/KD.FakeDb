using System;
using System.Collections.Generic;
using System.Linq;

namespace KD.FakeDb.Linq
{
    /// <summary>
    /// Additional Linq methods for all <see cref="IEnumerable{T}"/>.
    /// </summary>
    public static class Enumerable
    {
        /// <summary>
        /// <see cref="List{T}"/>'s ForEach method remade for all <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <typeparam name="TSource"> Source object type. </typeparam>
        /// <param name="source"> Input <see cref="IEnumerable{T}"/>. </param>
        /// <param name="action"> <see cref="Action"/> which should be performed on every element  </param>
        public static void ForEach<TSource>(this IEnumerable<TSource> source, Action<TSource> action)
        {
            source.ToList().ForEach(action);
        }

        /// <summary>
        /// Creates <see cref="HashSet{T}"/> from an <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static HashSet<TSource> ToHashSet<TSource>(this IEnumerable<TSource> source)
        {
            HashSet<TSource> result = new HashSet<TSource>();
            source.ForEach(element => result.Add(element));
            return result;
        }

        /// <summary>
        /// Creates <see cref="LinkedList{T}"/> from an <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static LinkedList<TSource> ToLinkedList<TSource>(this IEnumerable<TSource> source)
        {
            LinkedList<TSource> result = new LinkedList<TSource>();
            source.ForEach(element => result.AddLast(new LinkedListNode<TSource>(element)));
            return result;
        }

        /// <summary>
        /// Creates <see cref="Queue{T}"/> from an <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Queue<TSource> ToQueue<TSource>(this IEnumerable<TSource> source)
        {
            Queue<TSource> result = new Queue<TSource>();
            source.ForEach(element => result.Enqueue(element));
            return result;
        }

        /// <summary>
        /// Creates <see cref="SortedSet{T}"/> from an <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static SortedSet<TSource> ToSortedSet<TSource>(this IEnumerable<TSource> source)
        {
            SortedSet<TSource> result = new SortedSet<TSource>();
            source.ForEach(element => result.Add(element));
            return result;
        }

        /// <summary>
        /// Creates <see cref="Stack{T}"/> from an <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Stack<TSource> ToStack<TSource>(this IEnumerable<TSource> source)
        {
            Stack<TSource> result = new Stack<TSource>();
            source.ForEach(element => result.Push(element));
            return result;
        }
    }
}