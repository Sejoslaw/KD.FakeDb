using System.Text;

namespace KD.FakeDb.XML
{
    /// <summary>
    /// Various utils connected with operations on XML files.
    /// </summary>
    internal static class XmlUtils
    {
        /// <summary>
        /// Parses <see cref="IFakeTable"/> to it's XML representative.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        internal static string Parse(IFakeTable item)
        {
            StringBuilder builder = new StringBuilder();

            foreach (IFakeRow row in item.Rows)
            {
                builder.Append($"<Row>");
                foreach (IFakeColumn column in item.Columns)
                {
                    object columnValue = row[column.Name];
                    builder.Append($"<Column name=\"{ column.Name }\" value=\"{ columnValue }\" type=\"{ column.Type }\" />");
                }
                builder.Append($"</Row>");
            }

            string xml = builder.ToString();
            return xml;
        }
    }
}