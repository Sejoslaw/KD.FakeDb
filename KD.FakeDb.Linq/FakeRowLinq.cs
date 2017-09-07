namespace KD.FakeDb.Linq
{
    /// <summary>
    /// Describes Linq methods for <see cref="IFakeRow"/>.
    /// </summary>
    public static class FakeRowLinq
    {
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