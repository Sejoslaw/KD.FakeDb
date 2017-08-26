using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace KD.FakeDb.Linq
{
    /// <summary>
    /// Defines Linq methods for <see cref="IFakeColumn"/>.
    /// </summary>
    public static class FakeColumnLinq
    {
        /// <summary>
        /// Returns <see cref="List{T}"/> which contains all values from <see cref="IFakeColumn"/>.
        /// </summary>
        /// <typeparam name="TFakeColumn"> Type of input <see cref="IFakeColumn"/>. </typeparam>
        /// <typeparam name="TFakeColumnValueType"> Type to which values from this <see cref="IFakeColumn"/> should be cast. </typeparam>
        /// <param name="source"> Input <see cref="IFakeColumn"/>. </param>
        /// <returns></returns>
        public static List<KeyValuePair<int, TFakeColumnValueType>> ToList<TFakeColumn, TFakeColumnValueType>(this TFakeColumn source)
            where TFakeColumn : IFakeColumn
        {
            Type columnValuesType = source.Type;
            Type specifiedType = typeof(TFakeColumnValueType);

            if (columnValuesType.IsAssignableFrom(specifiedType)) throw new Exception(string.Format("Wrong specified type ({0}). Column values are type ({1})", specifiedType, columnValuesType));

            List<KeyValuePair<int, TFakeColumnValueType>> values = new List<KeyValuePair<int, TFakeColumnValueType>>();
            source.ToList().ForEach(element => values.Add(new KeyValuePair<int, TFakeColumnValueType>(element.Key, (TFakeColumnValueType)element.Value)));
            return values;
        }

        /// <summary>
        /// Performs the specified action on each <see cref="KeyValuePair{TKey, TValue}"/> record from this <see cref="IFakeColumn"/>.
        /// </summary>
        /// <typeparam name="TFakeColumn"> Type of the <see cref="IFakeColumn"/>. </typeparam>
        /// <param name="source"> Source <see cref="IFakeColumn"/>. </param>
        /// <param name="action"> <see cref="Action"/> which will be performed on each record in this <see cref="IFakeColumn"/>. </param>
        public static void ForEachInColumn<TFakeColumn>(this TFakeColumn source, Action<KeyValuePair<int, object>> action)
            where TFakeColumn : IFakeColumn
        {
            for (int i = 0; i < source.Count; ++i)
            {
                action(new KeyValuePair<int, object>(i, source[i]));
            }
        }
    }
}