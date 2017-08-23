﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace KD.FakeDb
{
    /// <summary>
    /// Default implementation of <see cref="IFakeTableCollection"/>.
    /// </summary>
    public class FakeTableCollection : IFakeTableCollection
    {
        /// <summary>
        /// For internal <see cref="IFakeTable"/>'s storing.
        /// </summary>
        private List<IFakeTable> tables = new List<IFakeTable>();

        public IFakeTable this[string tableName]
        {
            get
            {
                return GetTable(tableName);
            }
        }

        public int Count
        {
            get
            {
                return tables.Count;
            }
        }

        public IFakeDatabase Database { get; }

        public FakeTableCollection(IFakeDatabase fakeDatabase)
        {
            this.Database = fakeDatabase;
        }

        public IFakeTable AddTable(string tableName)
        {
            var tabs = (from tab in this.tables
                        where tab.Name.Equals(tableName)
                        select tab);

            if (tabs.Count() != 0)
            {
                throw new Exception(string.Format("Table with name \"{0}\" already exists.", tableName));
            }

            var newTable = new FakeTable(this.Database, tableName);
            this.tables.Add(newTable);
            return newTable;
        }

        public IFakeTable GetTable(string tableName)
        {
            var tabs = (from tab in this.tables
                        where tab.Name.Equals(tableName)
                        select tab);

            if (tabs.Count() != 1)
            {
                throw new Exception(string.Format("There is none or multiple Tables with name \"{0}\"", tableName));
            }

            return tables.First();
        }

        public void RemoveTable(string tableName)
        {
            var tabs = (from tab in this.tables
                        where tab.Name.Equals(tableName)
                        select tab);

            if (tabs.Count() != 1)
            {
                throw new Exception(string.Format("There is none or multiple Tables with name \"{0}\"", tableName));
            }

            tables.Remove(tabs.First());
        }

        public IEnumerator<IFakeTable> GetEnumerator()
        {
            return this.tables.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}