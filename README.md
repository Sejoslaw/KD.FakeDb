# KD.FakeDb

| Service  | Status |
|----------|--------|
| AppVeyor | [![Build status](https://ci.appveyor.com/api/projects/status/github/Sejoslaw/KD.FakeDb?svg=true)](https://ci.appveyor.com/api/projects/status/github/Sejoslaw/KD.FakeDb?svg=true) |
| Travis   | [![Build Status](https://travis-ci.org/Sejoslaw/KD.FakeDb.svg?branch=master)](https://travis-ci.org/Sejoslaw/KD.FakeDb) |

In-memory Fake Database. Made specially for Unit Tests.

PROJECTS:
---

Project Name | Description
-------------|-------------
**[KD.FakeDb](KD.FakeDb)** | Main project. Contains core interfaces and default implementation.
**[KD.FakeDb.Connection](KD.FakeDb.Connection)** | Contains generic definition for parsing existing Database to [IFakeDatabase](https://github.com/Sejoslaw/KD.FakeDb/blob/master/KD.FakeDb/IFakeDatabase.cs).
**[KD.FakeDb.Connection.MSSQL](KD.FakeDb.Connection.MSSQL)** | Contains implementation for parsing Microsoft SQL (MSSQL) Database to [IFakeDatabase](https://github.com/Sejoslaw/KD.FakeDb/blob/master/KD.FakeDb/IFakeDatabase.cs).
**[KD.FakeDb.Connection.MySQL](KD.FakeDb.Connection.MySQL)** | Contains implementation for parsing MySQL Database to [IFakeDatabase](https://github.com/Sejoslaw/KD.FakeDb/blob/master/KD.FakeDb/IFakeDatabase.cs).
**[KD.FakeDb.Factory](KD.FakeDb.Factory)** | Factory which should be used to dynamically create new [IFakeDatabase](https://github.com/Sejoslaw/KD.FakeDb/blob/master/KD.FakeDb/IFakeDatabase.cs).
**[KD.FakeDb.Linq](KD.FakeDb.Linq)** | Linq methods for [IFakeDatabase](https://github.com/Sejoslaw/KD.FakeDb/blob/master/KD.FakeDb/IFakeDatabase.cs).
**[KD.FakeDb.Serialization](KD.FakeDb.Serialization)** | Contains abstract definition for parsing [IFakeDatabase](https://github.com/Sejoslaw/KD.FakeDb/blob/master/KD.FakeDb/IFakeDatabase.cs).
**[KD.FakeDb.Serialization.JSON](KD.FakeDb.Serialization.JSON)** | JSON Serialization for [IFakeDatabase](https://github.com/Sejoslaw/KD.FakeDb/blob/master/KD.FakeDb/IFakeDatabase.cs).
**[KD.FakeDb.Serialization.XML](KD.FakeDb.Serialization.XML)** | XML Serialization for [IFakeDatabase](https://github.com/Sejoslaw/KD.FakeDb/blob/master/KD.FakeDb/IFakeDatabase.cs).
**[KD.FakeDb.XUnitTests](KD.FakeDb.XUnitTests)** | Tests made with xUnit Framework.
**[KD.FakeDb.XUnitTests.Connection](KD.FakeDb.XUnitTests.Connection)** | Tests with xUnit Framework made specially for different connections.


DONE:
---

- [X] Added Factory for static and dynamic Fake Database creation.
- [X] Finished including remade Linq methods (Almost any basic FakeDb object implements IEnumerable).
- [X] Added importing / exporting Fake Database to / from XML.
- [X] Added importing / exporting Fake Database to / from JSON.
- [X] Added converting existing MySQL Database to [IFakeDatabase](https://github.com/Sejoslaw/KD.FakeDb/blob/master/KD.FakeDb/IFakeDatabase.cs).
- [X] Added converting existing Microsoft SQL (MSSQL) Database to [IFakeDatabase](https://github.com/Sejoslaw/KD.FakeDb/blob/master/KD.FakeDb/IFakeDatabase.cs).

TODO:
---

- [ ] Add Events (OnCreate, OnUpdate, OnDelete, etc.) as an extension.
- [ ] Add serialization for [IFakeDatabase](https://github.com/Sejoslaw/KD.FakeDb/blob/master/KD.FakeDb/IFakeDatabase.cs) using [System.Data.DataSet](https://msdn.microsoft.com/en-us/library/system.data.dataset.aspx).
- [ ] Add importing / exporting Fake Database to / from Excel File Format or other File Format.
- [ ] Add importing / exporting Fake Database to / from CRM.
- [ ] Add support for [IFakeDatabase](https://github.com/Sejoslaw/KD.FakeDb/blob/master/KD.FakeDb/IFakeDatabase.cs) to read SQL Query.
- [ ] Add support for reading other Databases (for instance: Oracle, PostreSQL, MongoDB, DB2, Microsoft Access, Cassandra, Redis, Elasticsearch, SQLite, MariaDB, Sybase, Teradata, Firebird, Derby, etc.).
- [ ] Add support for Entity Framework to generate MDF file from [IFakeDatabase](https://github.com/Sejoslaw/KD.FakeDb/blob/master/KD.FakeDb/IFakeDatabase.cs).
- [ ] Add Proxy Generator for given [IFakeDatabase](https://github.com/Sejoslaw/KD.FakeDb/blob/master/KD.FakeDb/IFakeDatabase.cs). In source and .exe file.

TUTORIALS:
---

1. Create new Fake Database
```csharp
  // New Fake Database should be created using Factory
  IFakeDatabase database = FakeDatabaseFactory.NewDatabase("New Test Database Name");
```

2. Write Fake Database to XML (JSON looks similar)
```csharp
  public void Try_to_write_Database_to_XML()
  {
      var db = FakeDatabaseData.GetDatabaseWithData(); // Method used in tests to create new Fake Database and fill it with random data.
      var fileStream = new FileStream("db.xml", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
      using (var writer = XmlWriter.Create(fileStream))
      {
          var serializer = new FakeDbSerializer<XmlReader, XmlWriter>() // Serializer with given Xml Parameters, used to read / write Fake Database
          {
              Database = db, // Database which will be written to file
              Configuration = new FakeDbXMLByColumnConfiguration() // Default configuration used to read / write Fake Database
          };
          serializer.WriteDatabase(writer); // Write Fake Database to file
      }
  }
```

3. Read Fake Database from XML (JSON looks similar)
```csharp
  public void Try_to_read_Database_from_XML()
  {
      var fileStream = new FileStream("db.xml", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
      var serializer = new FakeDbSerializer<XmlReader, XmlWriter>() // Serializer with given Xml Parameters, used to read / write Fake Database
      {
          // Database property is not specified because it will be created dynamically
          Configuration = new FakeDbXMLByColumnConfiguration() // Default configuration used to read / write Fake Database
      };
      serializer.ReadDatabase(XDocument.Load(fileStream).CreateReader()); // Reads Fake Database and save it in serializer property
      
      var db = serializer.Database; // Database readed from XML file
  }
```

4. Read data from outside Database (MySQL, MSSQL, etc.). Used Database is MSSQL.
```csharp
  public void Test_if_database_was_mapped_to_Fake()
  {
      var dbConn = new DatabaseConnectionMSSQL() // Connection used to connect to MSSQL Database
      {
          Database = FakeDatabaseFactory.NewDatabase("Name_which_will_be_replaced_after_mapping"), // Fake Database must be given to Connector
          Connection = new SqlConnection() // Connection used to read data from Database
          {
              ConnectionString = $"" +
                  $"Server=mssql6.gear.host;" +
                  $"Database=testdb49;" +
                  $"User Id=testdb49;" +
                  $"Password=;"
          }
      };

      dbConn.ToFake("testdb49"); // This weird name is an actual Database Name. Database with this Name will be mapped to Connectors Fake Database.

      var fakeDb = dbConn.Database; // Fake Database filled with data taken from MSSQL Database.
  }
```
