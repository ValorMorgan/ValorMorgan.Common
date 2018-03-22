# ValorMorgan.Common #

> Common utilities and code paths for personal projects.

---
## Supported .NET Ecosystems ##
|Ecosystem       |Minimum Version|Maximum Version|
|:---------------|:-------------:|:-------------:|
|.NET Framework  |4.6.2          |*Latest*

---
## Application Values ##
Provides mapping logic for defining configuration values to a specific desired Type cast or default value.  Internally uses the Configuration utility to perform the retrieval and conversion of settings.  Can take in custom conversion delegates for casting a settings to a desired Type (i.e. casting a string to a KeyValuePair&lt;string, object&gt;).

### To register values ###
```csharp
var cache = new ValueCache();

// Register as required and typeof(string) (string is the default type)
cache.RegisterValue("<Setting Name Here>");

// Register as required and typeof(int)
cache.RegisterValue("<Setting Name Here>", typeof(int));

// Register as not required with a default value when not found
cache.RegisterValue(
    "<Setting Name Here>",
    typeof(string),
    isRequired: false,
    defaultValue: "<Default Value Here>"
);

// Register with a custom type cast
cache.RegisterValue(
    "<Setting Name Here>",
    typeof(AnyType),
    isRequired: true,
    s => new AnyType(s) // This can be any delegate returning typeof(AnyType)
);
```

### To get values from the cache ###
```csharp
// Without the type specified, we get an object return
object setting = cache.GetValue("<Setting Name Here">);

// With the type specified, we get that type
string settingAsString = cache.GetValue<string>("<Setting Name Here>");
int settingAsInt = cache.GetValue<int>("<Setting Name Here>");

// Even any custom type assuming the cache is registerd to handle the custom cast
var anyType = cache.GetValue<AnyType>("<Setting Name Here>");
```

### If you need to review the registry for a setting ###
```csharp
IValueRegistry registry = cache.GetSettingRegistry("<Setting Name Here>");

/* IValueRegistry
        Func<string, dynamic> CustomTypeCastFunction { get; set; }
        dynamic Default { get; set; }
        string Name { get; set; }
        bool Required { get; set; }
        Type ValueType { get; set; }
*/
```

---
## Configuration ##
Shim wrapping around ConfigurationManager from System.Configuration.  Contains logic for casting string returns from the configuration to other base types such as int and bool.  Also provides checks for confirming a setting is present and has a value.  Connection strings may also be retrieved through this utility.

### Getting settings ###
```csharp
string setting = ApplicationSettings.AsString("<Setting Name Here>");
int settingAsInt = ApplicationSettings.AsInt("<Setting Name Here>");
bool settingAsBool = ApplicationSettings.AsBool("<Setting Name Here>");

// Supports template type too though not very extensivly and likely to fail
var anyType = ApplicationSettings.As<AnyType>("<Setting Name Here>");
```

### Checking settings ###
```csharp
// Check if the setting even exists
ApplicationSettings.HasKey("<Setting Name Here>");

// Check if the setting has a value (will also check if the setting exists)
ApplicationSettings.HasValue("<Setting Name Here>");

// Throw an exception if the setting is not found
ApplicationSettings.ThrowExceptionIfKeyNotFound("<Setting Name Here>");

// Can also see available keys
string[] keys = ApplicationSettings.AllKeys;

// And the raw view of settings
NameValueCollection settings = ApplicationSettings.AppSettings;
```

### Getting connection strings ###
```csharp
string connectionString =
    ConnectionStrings.GetConnectionString("<Connection String Name Here>");
```

### Checking connection strings ###
```csharp
// Check if the connection string exists
ConnectionStrings.HasConnectionString("<Connection String Name Here>");

// Throw an exception if the connection string is not found
ConnectionStrings.ThrowExceptionIfConnectionStringNotFound(
    "<Connection String Name Here>"
);

// Can also see the raw view of connection strings
ConnectionStringSettingsCollection connectionStrings =
    ConnectionStrings.ConnectStrings;
```

---
## Error ##
Common logging utility for both writing messages and exceptions to the Windows Event Viewer.  Supports formatting exceptions as well.

>DISCLAIMER: I advise using other well established libraries such as [Serilog][Error_1] for this functionality.

[Error_1]: https://serilog.net/

### Setting Up the Logger ###
```csharp
// Via the constructor
ILogger logger = new Logger(
    logSource: "<Log Source>",
    logVerbose: false,
    logExceptionType: EventLogEntryType.Error,
    logMessageType: EventLogEntryType.Information,
    logExceptionId: 1234,
    logMessageId: 1234
);

// Via property injection
ILogger logger = new Logger
{
    LogSource = "<Log Source>",
    LogVerbose = false
    LogExceptionType = EventLogEntryType.Error,
    LogMessageType = EventLogEntryType.Information,
    LogExceptionId = 1234,
    LogMessageId = 1234
};

// Feel free to mix the above two to your heart's content

// You can also make use of the static logger
ValorMorgan.Common.Error.StaticLogger;
```

### Logging Exceptions & Messages ###

```csharp
var logger = ... // Setup like above

// Log a message (only works when LogVerbose = true)
logger.LogMessage("<Some Message>");

// Log an exception (just does Exception.ToString());
logger.LogException(new Exception());

// Log an exception with some special formatting
logger.LogFormattedException(new Exception());

// Can also log with the StaticLogger
Error.StaticLogger.LogException(
    new Exception(),
    logSource: "<Log Source>",
    logId: 1234,
    logEntryType: EventLogEntryType.Error
);

// There are default values for some settings as well
Error.StaticLogger.LogException(
    new Exception(),
    logSource: "<Log Source>"
    // logId = 0 by default
    // logEntryType = EventLogEntryType.Error by default
);

/* General formatted exception layout
============================================================
Type: <Exception Type>
Message: <Exception Message>
Logged On: <DateTime.Now>
============================================================
Stack Trace:
<Exception Stack Trace>
============================================================

Inner Exception:
============================================================
Type: <Inner Exception Type>
Message: <Inner Exception Message>
Logged On: <DateTime.Now>
============================================================
Stack Trace:
<Inner Exception Stack Trace>
============================================================

... Inner-most exception
*/
```

---
## Mail ##


---
## External Libraries Used ##
* [Castle.Core][1] (4.2.1)
* [MSTest.TestAdapter][2] (1.2.0)
* [MSTest.TestFramework][3] (1.2.0)
* [NSubstitute][4] (3.1.0)
* [NUnit][5] (3.10.1)
* [NUnit3TestAdapter][6] (3.10.0)
* [System.Threading.Tasks.Extensions][7] (4.4.0)

[1]: https://github.com/castleproject/Core
[2]: https://www.nuget.org/packages/MSTest.TestAdapter/
[3]: https://www.nuget.org/packages/MSTest.TestFramework/
[4]: http://nsubstitute.github.io/
[5]: http://nunit.org/
[6]: https://www.nuget.org/packages/NUnit3TestAdapter/
[7]: https://www.nuget.org/packages/System.Threading.Tasks.Extensions/