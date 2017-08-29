# KD.FakeDb

|Build|
|-----|
|[![Build status](https://ci.appveyor.com/api/projects/status/github/Sejoslaw/KD.FakeDb?svg=true)](https://ci.appveyor.com/api/projects/status/github/Sejoslaw/KD.FakeDb?svg=true)|

Fake Database. Made specially for Unit Tests.

Projects:
---

Project Name | Description
-------------|-------------
**[KD.FakeDb](KD.FakeDb)** | Main project. Contains core interfaces and default implementation.
**[KD.FakeDb.Factory](KD.FakeDb.Factory)** | Factory which should be used to dynamically create new Fake Database ([IFakeDatabase](https://github.com/Sejoslaw/KD.FakeDb/blob/master/KD.FakeDb/IFakeDatabase.cs)).
**[KD.FakeDb.Linq](KD.FakeDb.Linq)** | Linq methods made specially for Fake Database ([IFakeDatabase](https://github.com/Sejoslaw/KD.FakeDb/blob/master/KD.FakeDb/IFakeDatabase.cs)).
**[KD.FakeDb.XML](KD.FakeDb.XML)** | XML Serialization made for Fake Database. [FakeDbXMLSerializer](https://github.com/Sejoslaw/KD.FakeDb/blob/master/KD.FakeDb.XML/FakeDbXMLSerializer.cs) is the tool which reads / writes any [IFakeDatabase](https://github.com/Sejoslaw/KD.FakeDb/blob/master/KD.FakeDb/IFakeDatabase.cs) to / from XML File.
**[KD.FakeDb.XUnitTests](KD.FakeDb.XUnitTests)** | Tests made with xUnit Framework.


DONE:
---

- [X] Add Factory for static and dynamic Fake Database creation.
- [X] Finish including remade Linq methods (Almost any basic FakeDb object implements IEnumerable).
- [X] Added importing / exporting Fake Database to / from XML.

TODO:
---

- [ ] Added importing / exporting Fake Database to / from JSON.
- [ ] Added importing / exporting Fake Database to / from Excel File Format or other File Format.
- [ ] Added importing / exporting Fake Database to / from CRM.
- [ ] Add support for SQL Query and SqlClient.
- [ ] Generate SQL Database from Fake Database.
