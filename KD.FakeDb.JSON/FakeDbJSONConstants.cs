using System;
using System.Collections.Generic;
using System.Text;

namespace KD.FakeDb.JSON
{
    /// <summary>
    /// Contains constant JSON labels.
    /// </summary>
    public static class FakeDbJSONConstants
    {
        public static readonly string LabelDatabase = "Database";
        public static readonly string LabelTable = "Table";
        public static readonly string LabelColumn = "Column";
        public static readonly string LabelRow = "Row";
        public static readonly string LabelRecord = "Record";

        public static readonly string PropertyClass = "Class";
        public static readonly string PropertyName = "Name";
        public static readonly string PropertyCount = "Count";
        public static readonly string PropertyColumns = "Columns";
        public static readonly string PropertyRows = "Rows";
        public static readonly string PropertyIndex = "Index";
        public static readonly string PropertyValue = "Value";
        public static readonly string PropertyColumnRecordType = "ColumnRecordType";
    }
}