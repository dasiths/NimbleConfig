# NimbleConfig [![Build status](https://ci.appveyor.com/api/projects/status/4wbdssddl5qxukk7?svg=true)](https://ci.appveyor.com/project/dasiths/nimbleconfig) [![NuGet](https://img.shields.io/nuget/v/NimbleConfig.DependencyInjection.Aspnetcore.svg)](https://www.nuget.org/packages/NimbleConfig.DependencyInjection.Aspnetcore) [![Downloads](https://img.shields.io/nuget/dt/NimbleConfig.DependencyInjection.Aspnetcore.svg)](https://www.nuget.org/packages/NimbleConfig.DependencyInjection.Aspnetcore/)

### A simple, unambitious, convention-based configuration injector for .NET using IConfiguration (`Microsoft.Extensions.Configuration`) with full support for AspNetCore.
  
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
---
You can try this if you have to __access some configuration setting prior to setting up the DI__ container. (Be warned! This will create a instance of a factory for each call. Only do this if there is no other way.)

```C#
    // You still need to provide an instance of IConfiguration
    var dirtySetting = configuration.QuickReadSetting<SomeSetting>();
```

## Want more?

#### See the sample projects for more advanced use cases like complex types, enums and arrays. Checkout the `ConsoleApp` example on how to use it in a non aspnetcore app. 

NimbleConfig provides **full customisation** of the setting creation via **lifetime hooks** in `IConfigurationOptions`. This is done via creating your own resolvers for the name (`IKeyName`), reader (`IConfigurationReader`), parser (`IParser`), constructor (`IValueConstructor`).

__Example of setting a prefix uisng the configuration options lifetime hooks__
```C#

    var configOptions = ConfigurationOptions.Create()
                            .WithGlobalPrefix("MyAppSettings:") // Adding a global prefix to key names
                            .WithNamingScheme((type, name) => // Resolving type specific key names
                            {
                                if (type == typeof(SomeSetting)) // selectively apply logic
                                {
                                    return new KeyName("AnotherPrefix", name.QualifiedKeyName);
                                }
                         
                                return name; // return the auto-resolved one if no change is needed
                            });
    
    // Then just pass it in to the builder uisng the fluent api
	
    services.AddConfigurationSettings()
            .UsingOptionsIn(configOptions)
            .AndBuild();
```
						 
**These fluent apis allow you to easily add your custom logic.** They take a function which accepts a type and the auto-resolved instance as seen in the above example.
 
- `.WithNamingScheme()` for setting configuration key names.
- `.WithReader()` for setting a custom config reader.
- `.WithParser()` for setting a custom parser.
- `.WithConstructor()` for setting a custom value constructor.

---

Feel free to contribute and raise issues as you see fit :)

- Creator: Dasith Wijesiriwardena (http://dasith.me)
