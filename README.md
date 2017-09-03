# KD.FakeDb

| Service  | Status |
|----------|--------|
| AppVeyor | [![Build status](https://ci.appveyor.com/api/projects/status/github/Sejoslaw/KD.FakeDb?svg=true)](https://ci.appveyor.com/api/projects/status/github/Sejoslaw/KD.FakeDb?svg=true) |
| Travis   | [![Build Status](https://travis-ci.org/Sejoslaw/KD.FakeDb.svg?branch=master)](https://travis-ci.org/Sejoslaw/KD.FakeDb) |

Fake Database. Made specially for Unit Tests.

Projects:
---

Project Name | Description
-------------|-------------
**[KD.FakeDb](KD.FakeDb)** | Main project. Contains core interfaces and default implementation.
**[KD.FakeDb.Factory](KD.FakeDb.Factory)** | Factory which should be used to dynamically create new [IFakeDatabase](https://github.com/Sejoslaw/KD.FakeDb/blob/master/KD.FakeDb/IFakeDatabase.cs).
**[KD.FakeDb.Linq](KD.FakeDb.Linq)** | Linq methods for [IFakeDatabase](https://github.com/Sejoslaw/KD.FakeDb/blob/master/KD.FakeDb/IFakeDatabase.cs).
**[KD.FakeDb.Serialization](KD.FakeDb.Serialization)** | Generic serialization classes.
**[KD.FakeDb.Serialization.JSON](KD.FakeDb.Serialization.JSON)** | JSON Serialization for [IFakeDatabase](https://github.com/Sejoslaw/KD.FakeDb/blob/master/KD.FakeDb/IFakeDatabase.cs).
**[KD.FakeDb.Serialization.XML](KD.FakeDb.Serialization.XML)** | XML Serialization for [IFakeDatabase](https://github.com/Sejoslaw/KD.FakeDb/blob/master/KD.FakeDb/IFakeDatabase.cs).
**[KD.FakeDb.XUnitTests](KD.FakeDb.XUnitTests)** | Tests made with xUnit Framework.


DONE:
---

- [X] Add Factory for static and dynamic Fake Database creation.
- [X] Finish including remade Linq methods (Almost any basic FakeDb object implements IEnumerable).
- [X] Added importing / exporting Fake Database to / from XML.
- [X] Added importing / exporting Fake Database to / from JSON.

TODO:
---

- [ ] Added importing / exporting Fake Database to / from Excel File Format or other File Format.
- [ ] Added importing / exporting Fake Database to / from CRM.
- [ ] Add support for SQL Query and SqlClient.
- [ ] Generate SQL Database from Fake Database.
