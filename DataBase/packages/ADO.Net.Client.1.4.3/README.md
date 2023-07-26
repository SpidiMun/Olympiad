Version History
===============

### 1.4.3

* Updating to ADO.Net.Client.Implementation 1.4.3
* Adding ConnectionManager property to [DbClient](https://github.com/rgarrison12345/ADO.Net.Client/blob/master/src/ADO.Net.Client/DbClient.cs)

### 1.4.1.1

* Updating to ADO.Net.Client.Implementation 1.4.1.

### 1.4.1

* Updating to ADO.Net.Client.Implementation 1.4.1
* Adding .Net 5.0 build

### 1.4.0

* Updating to ADO.Net.Client.Implementation 1.4.0
* Adding .Net 4.0 build

### 1.3.5.3

* Updating to ADO.Net.Client.Implementation 1.3.5.3

### 1.3.5.2

* Updating to ADO.Net.Client.Implementation 1.3.5.2

### 1.3.5.1

* Updating to ADO.Net.Client.Implementation 1.3.5.1

### 1.3.5

* Updating to ADO.Net.Client.Implementation 1.3.5

### 1.3.4.1

* Updating to ADO.Net.Client.Implementation 1.3.4.1
 
### 1.3.4

* Updating to ADO.Net.Client.Implementation 1.3.4

### 1.3.3.2

* Updating to ADO.Net.Client.Implementation 1.3.3.2

### 1.3.3.1

* Updating to ADO.Net.Client.Implementation 1.3.3.1

### 1.3.3

* Updating to ADO.Net.Client.Implementation 1.3.3
 
### 1.3.2

* Updating to ADO.Net.Client.Implementation 1.3.2

### 1.3.1.1

* Updating to ADO.Net.Client.Implementation 1.3.1.1

### 1.3.1

* Updating to ADO.Net.Client.Implementation 1.3.1

### 1.3.0

* Updating to ADO.Net.Client.Implementation 1.3.0

### 1.2.0

* Updating to ADO.Net.Client.Implementation 1.2.0
* [DbClient](https://github.com/rgarrison12345/ADO.Net.Client/blob/master/src/ADO.Net.Client/DbAsynchronousClient.cs) adding two new methods
  * GetScalarValuesAsync
    * Returns an IEnumerable of values
  * GetScalarValuesStreamAsync for .Net 4.6.1 .Net Standard 2.0+ builds
    * Returns a single scalar streamed from a query
      * Intended to be implemented as an Iterator function
* [DbClient](https://github.com/rgarrison12345/ADO.Net.Client/blob/master/src/ADO.Net.Client/DbSynchronousClient.cs) adding two new methods
  * GetScalarValues
    * Returns an IEnumerable of values
  * GetScalarValuesStream
    * Returns a single scalar streamed from a query
      * Intended to be implemented as an Iterator function

### 1.1.9.2

* Updating to ADO.Net.Client.Implementation 1.1.9.2

### 1.1.9.1

* Updating to ADO.Net.Client.Implementation 1.1.9.1

### 1.1.9

* Updating to ADO.Net.Client.Implementation 1.1.9

### 1.1.8.2

* Updating to ADO.Net.Client.Implementation 1.1.8.2

### 1.1.8.1

* Updating to ADO.Net.Client.Implementation 1.1.8.1

### 1.1.8

* Updating to ADO.Net.Client.Implementation 1.1.8

### 1.1.7

* [DbClient](https://github.com/rgarrison12345/ADO.Net.Client/blob/master/src/ADO.Net.Client/DbClient.cs) now has two more constructors for ease of use of class
* Removing ExecuteTransactedNonQuery sync/async from [DbClient](https://github.com/rgarrison12345/ADO.Net.Client/blob/master/src/ADO.Net.Client/DbClient.cs)
* Updating to ADO.Net.Client.Implementation 1.1.7

### 1.1.6.1

* Updating to ADO.Net.Client.Implementation 1.1.6.1

### 1.1.6

* Updating to ADO.Net.Client.Implementation 1.1.6

### 1.1.5.2

* Updating to ADO.Net.Client.Implementation 1.1.5.3

### 1.1.5.1

* Updating to ADO.Net.Client.Implementation 1.1.5.2

### 1.1.5

* [DbClient](https://github.com/rgarrison12345/ADO.Net.Client/blob/master/src/ADO.Net.Client/DbSynchronousClient.cs) GetDbDataReader sync version now correctly utilizes command behavior parameter

### 1.1.4.1

* Updating to ADO.Net.Client.Implementation 1.1.5.1

### 1.1.4

* Updating to ADO.Net.Client.Implementation 1.1.5

### 1.1.3

* Upating to ADO.Net.Client.Implementation 1.1.4

### 1.1.2

* Updating to ADO.Net.Client.Implementations 1.1.3

### 1.1.0

* Updating to ADO.Net.Client.Implementation 1.1.2

### 1.0.9

* Updating to ADO.Net.Client.Implementation 1.1.1
* All DbClient GetScalarValue asyn/sync return T instead of object

### 1.0.8

* Uppdating to ADO.Net.Client.Implementation 1.1.0

### 1.0.7

* Uppdating to ADO.Net.Client.Implementation 1.0.9

### 1.0.6

* Uppdating to ADO.Net.Client.Implementation 1.0.6

### 1.0.5

* Updating async methods to use ShouldBePrepared from ISqlQuery that determines if a query 
should be prepared for .net standard 2.1 and above.  Updating to ADO.Net.Client.Implementation 1.0.5

### 1.0.4

* Updating to ADO.Net.Client.Implementation 1.0.4

### 1.0.3

* Updating to ADO.Net.Client.Implementation 1.0.3

### 1.0.2

* Initial release of package
 