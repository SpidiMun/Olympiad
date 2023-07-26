Version History
===============

### 1.4.3

* Updating to ADO.Net.Client.Core 1.4.4
* Adding [IConnectionManager](https://github.com/rgarrison12345/ADO.Net.Client/blob/master/src/ADO.Net.Client.Core/IConnectionManager.cs) as a getter property to the following objects
  to [SqlExecutor](https://github.com/rgarrison12345/ADO.Net.Client/blob/master/src/ADO.Net.Client.Implementation/SqlExecutor.cs)

### 1.4.2

* Removing .NET 4.0, 4.5 builds
* Adding .NET 6.0 build
* Updating to ADO.Net.Client.Core 1.4.3

### 1.4.1.1

* Updating to ADO.Net.Client.Core 1.4.2.1

### 1.4.1

* Updating to ADO.Net.Client.Core 1.4.2
* Adding .Net 5.0 build

### 1.4.0

* Updating to ADO.Net.Client.Core 1.4.0
* Adding .Net 4.0 build

### 1.3.5.3

* Updating to ADO.Net.Client.Core 1.3.6.2

### 1.3.5.2

* Updating to ADO.Net.Client.Core 1.3.6.1
* Using async dispose for .NET Standard 2.1+ builds where applicable

### 1.3.5.1

* Getxx and ExecuteNonQuery methods on [SqlExecutor](https://github.com/rgarrison12345/ADO.Net.Client/blob/master/src/ADO.Net.Client.Implementation/SqlExecutor.cs) are now virtual
* Methods on [MultiResultReader](https://github.com/rgarrison12345/ADO.Net.Client/blob/master/src/ADO.Net.Client.Implementation/MultiResultReader.cs) are now virtual

### 1.3.5

* Updating to ADO.Net.Client.Core 1.3.5
* [SqlExecutor](https://github.com/rgarrison12345/ADO.Net.Client/blob/master/src/ADO.Net.Client.Implementation/SqlExecutor.cs) properly using [CancellationToken](https://docs.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken?view=netcore-3.1) in GetScalarValuesAsync and GetScalarValuesStreamAsync

### 1.3.4.1

* Updating to ADO.Net.Client.Core 1.3.5.1

### 1.3.4

* Updating to ADO.Net.Client.Core 1.3.5

### 1.3.3.2

* GetScalarValues sync/async now returns default value of T if value from database is [DBNull.Value](https://docs.microsoft.com/en-us/dotnet/api/system.dbnull.value?view=netcore-3.1)
* GetScalarValuesStream sync/async now returns default value of T if value from database is [DbNull.Value](https://docs.microsoft.com/en-us/dotnet/api/system.dbnull.value?view=netcore-3.1)
 
### 1.3.3.1

* Updating to ADO.Net.Client.Core 1.3.4.1

### 1.3.3

* Updating to ADO.Net.Client.Core 1.3.4

### 1.3.2

* Updating to ADO.Net.Client.Core 1.3.3

### 1.3.1.1

* Updating to ADO.Net.Client.Core 1.3.2.1

### 1.3.1

* Updating to ADO.Net.Client.Core 1.3.2

### 1.3.0

* Updating to ADO.Net.Client.Core 1.3.0
* New class [AdHocQueryBuilder](https://github.com/rgarrison12345/ADO.Net.Client/blob/master/src/ADO.Net.Client.Implementation/AdHocQueryBuilder.cs) for creating ad-hoc queries
* New class [StoredProcedureQueryBuilder](https://github.com/rgarrison12345/ADO.Net.Client/blob/master/src/ADO.Net.Client.Implementation/StoredProcedureQueryBuilder.cs) for creating stored procedure queries
* SqlQuery moved to ADO.Net.Client.Core
* QueryBuilder moved to ADO.Net.Client.Core
 
### 1.2.0

* Updating to ADO.Net.Client.Core 1.2.0
* [SqlExecutor](https://github.com/rgarrison12345/ADO.Net.Client/blob/master/src/ADO.Net.Client.Implementation/SqlExecutorAsync.cs) adding two new methods
  * GetScalarValuesAsync
    * Returns an IEnumerable of values
  * GetScalarValuesStreamAsync for .Net 4.6.1 .Net Standard 2.0+ builds
    * Returns a single scalar streamed from a query
      * Intended to be implemented as an Iterator function
* [SqlExecutor](https://github.com/rgarrison12345/ADO.Net.Client/blob/master/src/ADO.Net.Client.Implementation/SqlExecutorSync.cs) adding two new methods
  * GetScalarValues
    * Returns an IEnumerable of values
  * GetScalarValuesStream
    * Returns a single scalar streamed from a query
      * Intended to be implemented as an Iterator function

### 1.1.9.2

* Updating to ADO.Net.Client.Core 1.1.9.4

### 1.1.9.1

* Updating to ADO.Net.Client.Core 1.1.9.3

### 1.1.9

* Updating to ADO.Net.Client.Core 1.1.9.2

### 1.1.8.2

* [MultiResultReader](https://github.com/rgarrison12345/ADO.Net.Client/blob/master/src/ADO.Net.Client.Implementation/MultiResultReader.cs) GetDataObject sync/async now return default for the type parameter if no data could be read

### 1.1.8.1

* Updating to ADO.Net.Client.Core 1.1.9.1

### 1.1.8

* Updating to ADO.Net.Client.Core 1.1.9

### 1.1.7

* [SqlExecutor](https://github.com/rgarrison12345/ADO.Net.Client/blob/master/src/ADO.Net.Client.Implementation/SqlExecutor.cs) new constructor for ease of use of class
* Updating to ADO.Net.Client.Core 1.1.8
* Removing ExecuteTransactedNonQuery sync/async versions

### 1.1.6.1

* [SqlExecutor](https://github.com/rgarrison12345/ADO.Net.Client/blob/master/src/ADO.Net.Client.Implementation/SqlExecutor.cs) GetMultiResultReaderAsync now utilizes the passed in CancellationToken

### 1.1.6

* Updating to ADO.Net.Client.Core 1.1.7
* [MultiResultReader](https://github.com/rgarrison12345/ADO.Net.Client/blob/master/src/ADO.Net.Client.Implementation/MultiResultReader.cs) has a new constructor for ease of use of the class

### 1.1.5.3

* Updating to ADO.Net.Client.Core 1.1.6.3

### 1.1.5.2

* Updating to ADO.Net.Client.Core 1.1.6.2

### 1.1.5.1

* Updating to ADO.Net.Client.Core 1.1.6.1

### 1.1.5

* Updating to ADO.Net.Client.Core 1.1.6

### 1.1.4

* Updating to ADO.Net.Client.Core 1.1.5
* [QueryBuilder](https://github.com/rgarrison12345/ADO.Net.Client/blob/master/src/ADO.Net.Client.Implementation/QueryBuilder.cs) CreateSQLQuery now has optional parameter to clear 
  parameters and query text used to buld an instance of [ISqlQuery](https://github.com/rgarrison12345/ADO.Net.Client/blob/master/src/ADO.Net.Client.Core/ISqlQuery.cs)

### 1.1.3

* [QueryBuilder](https://github.com/rgarrison12345/ADO.Net.Client/blob/master/src/ADO.Net.Client.Implementation/QueryBuilder.cs) Constructor now takes in an instance of [IDbObjectFactory](https://github.com/rgarrison12345/ADO.Net.Client/blob/master/src/ADO.Net.Client.Core/IDbObjectFactory.cs)
* Adding AddParameterRange overload to [QueryBuilder](https://github.com/rgarrison12345/ADO.Net.Client/blob/master/src/ADO.Net.Client.Implementation/QueryBuilder.cs) that takes in a parameter array of objects
* Adding Append overload to [QueryBuilder](https://github.com/rgarrison12345/ADO.Net.Client/blob/master/src/ADO.Net.Client.Implementation/QueryBuilder.cs) that takes in a SQL string in a parameter array of objects
* Adding append overload to [QueryBuilder](https://github.com/rgarrison12345/ADO.Net.Client/blob/master/src/ADO.Net.Client.Implementation/QueryBuilder.cs) that takes in a parameter name and value
* Updating to ADO.Net.Client.Core 1.1.4

### 1.1.2

* Updating to ADO.Net.Client.Core 1.1.3

### 1.1.1

* Updating to ADO.Net.Client.Core 1.1.2
* All SqlExecutor GetScalarValue asyn/sync return T instead of object

### 1.1.0

* Updating to ADO.Net.Client.Core 1.1.1
* Adding GetMultiResultReader async/sync on SqlExecutor
 
### 1.0.9

* Updating to ADO.Net.Client.Core 1.1.0

### 1,0.8

* Updating to ADO.Net.Client.Core 1.0.9
* QueryBuilder class now has Contains function that takes in an instance of DbParameter

### 1.0.7

* Renaming ReadObjectList and ReadObjectEnumerable on MultiResultReader
  * Renamed to ReadObjects
  * Renamed to ReadObjectsStream
* Renaming ReadObjectListAsync and ReadObjectEnumerableAsync on MultiResultReader
  * Renamed to ReadObjectsAsync
  * Renamed to ReadaObjectsStreamAsync
* MultiResultReader constructor now takes in an instance of IDataMapper
* SqlExecutor constructor now also takes in an instance of IDataMapper
* SqlExecutor GetScalarValue sync/async methods are now generic
* Updating to ADO.Net.Client.Core 1.0.8
* Adding .NET 4.5, 4.6.1, removing .NET 4.7.2 build

### 1.0.6

* Updating to ADO.Net.Client.Core 1.0.6 
* Removing IConnectionStringUtility methods from DbConnectionManager
* Adding new ConnectionStringBuilder class

### 1.0.5

* Updating SqlExecutor async methods to take in a bool that determines if a query should be prepared
  * .NET standard 2.1 and above.  
* Updating to ADO.Net.Client.Core 1.0.5

### 1.0.4

* Updating SqlExecutor sync methods to take in a bool that determines if a query should be prepared
* Updating to ADO.Net.Client.Core 1.0.4

### 1.0.3

### 1.0.2

* Initial Release
