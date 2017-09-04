# KD.FakeDb

| Service  | Status |
|----------|--------|
| AppVeyor | [![Build status](https://ci.appveyor.com/api/projects/status/github/Sejoslaw/KD.FakeDb?svg=true)](https://ci.appveyor.com/api/projects/status/github/Sejoslaw/KD.FakeDb?svg=true) |
| Travis   | [![Build Status](https://travis-ci.org/Sejoslaw/KD.FakeDb.svg?branch=master)](https://travis-ci.org/Sejoslaw/KD.FakeDb) |

In-memory Fake Database. Made specially for Unit Tests.

Projects:
---

Project Name | Description
-------------|-------------
**[KD.FakeDb](KD.FakeDb)** | Main project. Contains core interfaces and default implementation.
**[KD.FakeDb.Connection](KD.FakeDb.Connection)** | Contains generic definition for parsing existing Database to [IFakeDatabase](https://github.com/Sejoslaw/KD.FakeDb/blob/master/KD.FakeDb/IFakeDatabase.cs).
**[KD.FakeDb.Connection.MSSQL](KD.FakeDb.Connection.MSSQL)** | Contains implementation for parsing Microsoft SQL (MSSQL) Database to [IFakeDatabase](https://github.com/Sejoslaw/KD.FakeDb/blob/master/KD.FakeDb/IFakeDatabase.cs).
**[KD.FakeDb.Connection.MySQL](KD.FakeDb.Connection.MySQL)** | Contains implementation for parsing MySQL Database to [IFakeDatabase](https://github.com/Sejoslaw/KD.FakeDb/blob/master/KD.FakeDb/IFakeDatabase.cs).
**[KD.FakeDb.Factory](KD.FakeDb.Factory)** | Factory which should be used to dynamically create new [IFakeDatabase](https://github.com/Sejoslaw/KD.FakeDb/blob/master/KD.FakeDb/IFakeDatabase.cs).
**[KD.FakeDb.Linq](KD.FakeDb.Linq)** | Linq methods for [IFakeDatabase](https://github.com/Sejoslaw/KD.FakeDb/blob/master/KD.FakeDb/IFakeDatabase.cs).
**[KD.FakeDb.Serialization](KD.FakeDb.Serialization)** | Contains generic definition for parsing [IFakeDatabase](https://github.com/Sejoslaw/KD.FakeDb/blob/master/KD.FakeDb/IFakeDatabase.cs).
**[KD.FakeDb.Serialization.JSON](KD.FakeDb.Serialization.JSON)** | JSON Serialization for [IFakeDatabase](https://github.com/Sejoslaw/KD.FakeDb/blob/master/KD.FakeDb/IFakeDatabase.cs).
**[KD.FakeDb.Serialization.XML](KD.FakeDb.Serialization.XML)** | XML Serialization for [IFakeDatabase](https://github.com/Sejoslaw/KD.FakeDb/blob/master/KD.FakeDb/IFakeDatabase.cs).
**[KD.FakeDb.XUnitTests](KD.FakeDb.XUnitTests)** | Tests made with xUnit Framework.
**[KD.FakeDb.XUnitTests.Connection](KD.FakeDb.XUnitTests.Connection)** | Tests with xUnit Framework made specially for different connections.


DONE:
---

- [X] Add Factory for static and dynamic Fake Database creation.
- [X] Finish including remade Linq methods (Almost any basic FakeDb object implements IEnumerable).
- [X] Added importing / exporting Fake Database to / from XML.
- [X] Added importing / exporting Fake Database to / from JSON.
- [X] Added converting existing MySQL Database to [IFakeDatabase](https://github.com/Sejoslaw/KD.FakeDb/blob/master/KD.FakeDb/IFakeDatabase.cs).
- [X] Added converting existing Microsoft SQL (MSSQL) Database to [IFakeDatabase](https://github.com/Sejoslaw/KD.FakeDb/blob/master/KD.FakeDb/IFakeDatabase.cs).

TODO:
---

- [ ] Added importing / exporting Fake Database to / from Excel File Format or other File Format.
- [ ] Added importing / exporting Fake Database to / from CRM.
- [ ] Add support for [IFakeDatabase](https://github.com/Sejoslaw/KD.FakeDb/blob/master/KD.FakeDb/IFakeDatabase.cs) to read SQL Query.
- [ ] Add support for reading other Databases (for instance: Oracle, PostreSQL, MongoDB, DB2, Microsoft Access, Cassandra, Redis, Elasticsearch, SQLite, etc.).
