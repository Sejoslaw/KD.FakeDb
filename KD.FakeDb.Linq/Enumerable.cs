using System;
using System.Collections.Generic;
using System.Linq;

namespace KD.FakeDb.Linq
{
    /// <summary>
    /// Additional Linq methods for <see cref="IEnumerable{T}"/>.
    /// </summary>
    public static class Enumerable
    {
        /// <summary>
        /// <see cref="List{T}"/>'s ForEach method remade for all <see cref="IEnumerable{T}"/>'s.
        /// </summary>
        /// <typeparam name="TSource"> Source object type. </typeparam>
        /// <param name="source"> Input <see cref="IEnumerable{T}"/>. </param>
        /// <param name="action"> <see cref="Action"/> which should be performed on every element  </param>
        public static void ForEach<TSource>(this IEnumerable<TSource> source, Action<TSource> action)
        {
            source.ToList().ForEach(action);
        }
    }
}