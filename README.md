# InMemoryDatabase

A lightweight in memory database for C#.
InMemoryDatabase is intended to be used on development and testing environments only.
Do not use it on production environments, as its data is bounded to a single instance and will be lost when this instance ends.

## Installation

[![Nuget](https://img.shields.io/nuget/v/InMemoryDatabase.svg)](https://www.nuget.org/packages/InMemoryDatabase)

InMemoryDatabase runs on Windows, Linux, and macOS using [.NET Core](https://github.com/dotnet/core).

You can install the InMemoryDatabase NuGet package from the .NET Core CLI using:
```
dotnet add package InMemoryDatabase
```

or from the NuGet package manager:
```
Install-Package InMemoryDatabase
```

Or alternatively, you can add the InMemoryDatabase package from within Visual Studio's NuGet package manager or via [Paket](https://github.com/fsprojects/Paket).

## Usage

### Identifiers

To be persisted on InMemoryDatabase, a class must have one or more properties tagged as Identifiers, as below.

```C#
using InMemoryDatabase.Attibutes;

public class MyPersistedObject
{
     [Identifier]
     public int Id { get; set; }
     
     public string Name { get; set; }
}
```

If more than one property will be used to form a conpund key, an order should be specified, as below.

```C#
using InMemoryDatabase.Attibutes;

public class MyPersistedObject
{
     [Identifier]
     public string FirstName { get; set; }
     
     [Identifier(2)]
     public string LastName { get; set; }
     
     public int Age { get; set; }
}
```

### Registering the Collection

The collections can be registered at startup, on the ConfigureServices method.

```C#
using InMemoryDatabase.Setup;

// This method gets called by the runtime. Use this method to add services to the container.
public void ConfigureServices(IServiceCollection services)
{
    services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
    services.SetupInMemoryCollection<Student>();
}
```

More information can be found within our [samples](https://github.com/GuzzoLM/InMemoryDatabase/tree/master/samples)

## License

InMemoryDatabase is licensed under the [MIT license](LICENSE).
