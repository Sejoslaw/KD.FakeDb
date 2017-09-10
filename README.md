# KD.FakeDb

| Service  | Status |
|----------|--------|
| AppVeyor | [![Build status](https://ci.appveyor.com/api/projects/status/github/Sejoslaw/KD.FakeDb?svg=true)](https://ci.appveyor.com/api/projects/status/github/Sejoslaw/KD.FakeDb?svg=true) |
| Travis   | [![Build Status](https://travis-ci.org/Sejoslaw/KD.FakeDb.svg?branch=master)](https://travis-ci.org/Sejoslaw/KD.FakeDb) |

In-memory Fake Database. Made specially for Unit Tests.

PROJECTS:
---

Project Name / Namespace Name | Description
-------------|-------------
**[KD.FakeDb](KD.FakeDb)** | Main project. Contains core interfaces and default implementation.
**[KD.FakeDb.Connection](KD.FakeDb.Connection)** | Contains generic definition for parsing existing Database to [IFakeDatabase](https://github.com/Sejoslaw/KD.FakeDb/blob/master/KD.FakeDb/IFakeDatabase.cs).
**[KD.FakeDb.Connection.MSSQL](KD.FakeDb.Connection.MSSQL)** | Contains implementation for parsing Microsoft SQL (MSSQL) Database to [IFakeDatabase](https://github.com/Sejoslaw/KD.FakeDb/blob/master/KD.FakeDb/IFakeDatabase.cs).
**[KD.FakeDb.Connection.MySQL](KD.FakeDb.Connection.MySQL)** | Contains implementation for parsing MySQL Database to [IFakeDatabase](https://github.com/Sejoslaw/KD.FakeDb/blob/master/KD.FakeDb/IFakeDatabase.cs).
**[KD.FakeDb.Converter.DataSet](KD.FakeDb.Converter.DataSet)** | Extension method for [IFakeDatabase](https://github.com/Sejoslaw/KD.FakeDb/blob/master/KD.FakeDb/IFakeDatabase.cs) to convert it to and from [System.Data.DataSet](https://msdn.microsoft.com/en-us/library/system.data.dataset.aspx).
**[KD.FakeDb.Export](KD.FakeDb.Export)** | Contains generic definition of [IFakeDatabase](https://github.com/Sejoslaw/KD.FakeDb/blob/master/KD.FakeDb/IFakeDatabase.cs)'s Exporter.
**[KD.FakeDb.Export.Files](KD.FakeDb.Export.Files)** | [IFakeDatabase](https://github.com/Sejoslaw/KD.FakeDb/blob/master/KD.FakeDb/IFakeDatabase.cs)'s Exporter pre-configured for exporting to Files.
**[KD.FakeDb.Export.Files.CSV](KD.FakeDb.Export.Files.CSV)** | Contains implementation for exporting [IFakeDatabase](https://github.com/Sejoslaw/KD.FakeDb/blob/master/KD.FakeDb/IFakeDatabase.cs) to CSV file.
**[KD.FakeDb.Factory](KD.FakeDb.Factory)** | Factory which should be used to dynamically create new [IFakeDatabase](https://github.com/Sejoslaw/KD.FakeDb/blob/master/KD.FakeDb/IFakeDatabase.cs).
**[KD.FakeDb.Linq](KD.FakeDb.Linq)** | Linq methods for [IFakeDatabase](https://github.com/Sejoslaw/KD.FakeDb/blob/master/KD.FakeDb/IFakeDatabase.cs).
**[KD.FakeDb.Serialization](KD.FakeDb.Serialization)** | Contains abstract definition for parsing [IFakeDatabase](https://github.com/Sejoslaw/KD.FakeDb/blob/master/KD.FakeDb/IFakeDatabase.cs).
**[KD.FakeDb.Serialization.DataSet](KD.FakeDb.Serialization.DataSet)** | [System.Data.DataSet](https://msdn.microsoft.com/en-us/library/system.data.dataset.aspx) Serialization for [IFakeDatabase](https://github.com/Sejoslaw/KD.FakeDb/blob/master/KD.FakeDb/IFakeDatabase.cs).
**[KD.FakeDb.Serialization.JSON](KD.FakeDb.Serialization.JSON)** | JSON Serialization for [IFakeDatabase](https://github.com/Sejoslaw/KD.FakeDb/blob/master/KD.FakeDb/IFakeDatabase.cs).
**[KD.FakeDb.Serialization.XML](KD.FakeDb.Serialization.XML)** | XML Serialization for [IFakeDatabase](https://github.com/Sejoslaw/KD.FakeDb/blob/master/KD.FakeDb/IFakeDatabase.cs).
**[KD.FakeDb.XUnitTests](KD.FakeDb.XUnitTests)** | Tests made with xUnit Framework.
**[KD.FakeDb.XUnitTests.Connection](KD.FakeDb.XUnitTests.Connection)** | Tests with xUnit Framework made specially for different connections.

DEPENDENCY DIAGRAM (Where "Externals" are .NET Core libraries and Project-Required libraries. For example: "Newtonsoft.Json" for "KD.FakeDb.Serialization.JSON"):
---

![](https://raw.githubusercontent.com/Sejoslaw/KD.FakeDb/master/img/Dependency/Dependency%20Diagram%202.PNG)

DONE:
---

- [X] Added Factory for static and dynamic Fake Database creation.
- [X] Finished including remade Linq methods (Almost any basic FakeDb object implements IEnumerable).
- [X] Added importing / exporting [IFakeDatabase](https://github.com/Sejoslaw/KD.FakeDb/blob/master/KD.FakeDb/IFakeDatabase.cs) to / from XML.
- [X] Added importing / exporting [IFakeDatabase](https://github.com/Sejoslaw/KD.FakeDb/blob/master/KD.FakeDb/IFakeDatabase.cs) to / from JSON.
- [X] Added importing / exporting [IFakeDatabase](https://github.com/Sejoslaw/KD.FakeDb/blob/master/KD.FakeDb/IFakeDatabase.cs) to / from [System.Data.DataSet](https://msdn.microsoft.com/en-us/library/system.data.dataset.aspx).
- [X] Added converting existing MySQL Database to [IFakeDatabase](https://github.com/Sejoslaw/KD.FakeDb/blob/master/KD.FakeDb/IFakeDatabase.cs).
- [X] Added converting existing Microsoft SQL (MSSQL) Database to [IFakeDatabase](https://github.com/Sejoslaw/KD.FakeDb/blob/master/KD.FakeDb/IFakeDatabase.cs).
- [X] Added exporting [IFakeDatabase](https://github.com/Sejoslaw/KD.FakeDb/blob/master/KD.FakeDb/IFakeDatabase.cs) to CSV File Format.

TODO:
---

- [ ] Add Events (OnCreate, OnUpdate, OnDelete, etc.) as an extension.
- [ ] Add exporting [IFakeDatabase](https://github.com/Sejoslaw/KD.FakeDb/blob/master/KD.FakeDb/IFakeDatabase.cs) to Excel File Format.
- [ ] Add importing / exporting [IFakeDatabase](https://github.com/Sejoslaw/KD.FakeDb/blob/master/KD.FakeDb/IFakeDatabase.cs) to / from CRM.
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

1.1. Add data to Fake Database
```csharp
  public void Add_some_data_to_FakeDatabase()
  {
      // Create new Fake Database
      IFakeDatabase db = FakeDatabaseFactory.NewDatabase("Test Database");
      
      // Create and return new Fake Table named "Accounts"
      IFakeTable accTable = db.AddTable("Accounts");
      
      // Add few Fake Columns to Fake Table
      // Specify Fake Columns Name and Type of data which can be added to Column
      accTable.AddColumn("AccountId", typeof(Guid));
      accTable.AddColumn("FirstName", typeof(string));
      accTable.AddColumn("LastName", typeof(string));
      accTable.AddColumn("CountryName", typeof(string));
      
      // Add few Fake Rows to Fake Table
      
      // Fake Row is created and returned dynamically
      // You can add values to cells using Column Names
      // 0 is an index of a new Fake Row
      // If Fake Row with index 0 does not exists it will be created in real time; otherwise it will be returned
      IFakeRow fakeRow = accTable.GetRow(0); 
      fakeRow["AccountId"] = Guid.NewGuid();
      fakeRow["FirstName"] = "Krzysztof";
      fakeRow["LastName"] = "Dobrzynski";
      fakeRow["CountryName"] = "Poland";
      
      // You can also fill Cells using right Cell index
      fakeRow = accTable.GetRow(1);
      fakeRow[0] = Guid.NewGuid();
      fakeRow[1] = "Esteban";
      fakeRow[2] = "Hulio";
      fakeRow[3] = "Spain";
      
      fakeRow = accTable.GetRow(2);
      fakeRow["AccountId"] = Guid.NewGuid();
      fakeRow["FirstName"] = "A";
      fakeRow["LastName"] = "B";
      fakeRow["CountryName"] = "C";
      
      //...
  }
```

2. Write Fake Database to XML (JSON looks similar)
```csharp
  public void Try_to_write_Database_to_XML()
  {
      IFakeDatabase db = FakeDatabaseData.GetDatabaseWithData(); // Method used in tests to create new Fake Database and fill it with random data.
      FileStream fileStream = new FileStream("db.xml", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
      using (XmlWriter writer = XmlWriter.Create(fileStream))
      {
          var serializer = new FakeDbSerializer<XmlReader, XmlWriter>() // Serializer with given Xml Parameters, used to read / write Fake Database
          {
              Database = db, // Database which will be written to file
              Configuration = new FakeDbXMLByColumnConfiguration() // Default configuration used to read / write Fake Database to / from XML File.
          };
          serializer.WriteDatabase(writer); // Write Fake Database to file
      }
  }
```

3. Read Fake Database from XML (JSON looks similar)
```csharp
  public void Try_to_read_Database_from_XML()
  {
      FileStream fileStream = new FileStream("db.xml", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
      var serializer = new FakeDbSerializer<XmlReader, XmlWriter>() // Serializer with given Xml Parameters, used to read / write Fake Database
      {
          // Database property is not specified because it will be created dynamically
          Configuration = new FakeDbXMLByColumnConfiguration() // Default configuration used to read / write Fake Database to / from XML File.
      };
      serializer.ReadDatabase(XDocument.Load(fileStream).CreateReader()); // Reads Fake Database and save it in serializer property
      
      IFakeDatabase db = serializer.Database; // Database readed from XML file
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

      IFakeDatabase fakeDb = dbConn.Database; // Fake Database filled with data taken from MSSQL Database.
  }
```

5. Convert [IFakeDatabase](https://github.com/Sejoslaw/KD.FakeDb/blob/master/KD.FakeDb/IFakeDatabase.cs) to [System.Data.DataSet](https://msdn.microsoft.com/en-us/library/system.data.dataset.aspx)
```csharp
  using KD.FakeDb.Converter.DataSet;
  
  //...
  
  public void Test_if_IFakeDatabase_was_converted_to_DataSet()
  {
      IFakeDatabase fakeDb = FakeDatabaseFactory.NewDatabase("New Test Database Name"); // New Fake Database
      
      //...Fill Fake Database with some data...
      
      System.Data.DataSet dataSet = fakeDb.ToDataSet(); // Convert Fake Database to DataSet
      
      //...
  }
```

6. Convert [System.Data.DataSet](https://msdn.microsoft.com/en-us/library/system.data.dataset.aspx) to [IFakeDatabase](https://github.com/Sejoslaw/KD.FakeDb/blob/master/KD.FakeDb/IFakeDatabase.cs)
```csharp
  using KD.FakeDb.Converter.DataSet;
  
  //...
  
  public void Test_if_DataSet_was_converted_to_IFakeDatabase(System.Data.DataSet dataSet) // DataSet which will be converted to IFakeDatabase
  {
      // Create new IFakeDatabase
      IFakeDatabase fakeDatabase = FakeDatabaseFactory.NewDatabase("Some random Database name that will be replaced after fill from DataSet.");
      
      // Fill IFakeDatabase with DataSet values
      dataSet.ToFakeDatabase(fakeDatabase);
      
      //...Now IFakeDatabase is filled with values readed from DataSet...
  }
```
