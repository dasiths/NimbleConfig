# NimbleConfig [![Build status](https://ci.appveyor.com/api/projects/status/4wbdssddl5qxukk7?svg=true)](https://ci.appveyor.com/project/dasiths/nimbleconfig) [![NuGet](https://img.shields.io/nuget/v/NimbleConfig.DependencyInjection.Aspnetcore.svg)](https://www.nuget.org/packages/NimbleConfig.DependencyInjection.Aspnetcore)

### A simple, unambitious, convention-based configuration injector for .net standard using IConfiguration (`Microsoft.Extensions.Configuration`) with full support for AspNetCore.
  
---

## Getting Started

1. Install and reference the Nuget `NimbleConfig.DependencyInjection.Aspnetcore`

In the NuGet Package Manager Console, type:

```
    Install-Package NimbleConfig.DependencyInjection.Aspnetcore
```

2. Define your settings class as follows
```C#
    // Our setting is a string
    public class SomeSetting: ConfigurationSetting<string>
    {
    }
	
    // or for a more complex type
	
    public class SomeComplexSetting : IComplexConfigurationSetting
    {
        public string SomeProperty { get; set; }
    }
```
3. Add it to your `appsettings.json`
```C#
    {
        "SomeSetting": "SomeValue",
        "SomeComplexSetting": {
            "SomeProperty": "SomeValue"
        }
    }
```
4. Inject and use it in your controllers, services etc
```C#
    public class ValuesController : ControllerBase
    {
        private readonly SomeSetting _someSetting;
        private readonly SomeComplexSetting _someComplexSetting;
		
        public ValuesController(SomeSetting someSetting, SomeComplexSetting someComplexSetting)
        {
            _someSetting = someSetting;
            _someComplexSetting = someComplexSetting;
        }
		
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { 
                _someSetting.Value,
                _someComplexSetting.SomeProperty
            };
        }
    }
```
5. In the `ConfigureServices()` method in your `Startup.cs` add the following to scan and inject settings types
```C#
    public void ConfigureServices(IServiceCollection services)
    {
        // Other services go here
		
        // Wire it up using the fluent api
        services.AddConfigurationSettings().AndBuild();
    }
```

## Want more?

#### See the sample projects for more advanced use cases like complex types, enums and arrays. Checkout the `ConsoleApp` example on how to use it in a non aspnetcore app. 

#### NimbleConfig provides full customisation of resolving the name (`IKeyName`), reader (`IConfigurationReader`), parser (`IParser`), constructor (`IValueConstructor`) for your setting. It also allows hooks in to the setting creation life cycle via the `ConfigurationOptions`.

---

Feel free to contribute and raise issues as you see fit :)

- Creator: Dasith Wijesiriwardena (http://dasith.me)
