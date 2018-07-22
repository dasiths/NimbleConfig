## NimbleConfig ##
### A simple, unambitious, convention-based configuration injector for .net with support for AspNetCore using IConfiguration (`Microsoft.Extensions.Configuration`)
  
---

### Getting Started ###

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
```
3. Add it to your `appsettings.json`
```C#
    {
        "SomeSetting": "SomeValue"
    }
```
4. Inject and use it in your controllers, services etc
```C#
    public class ValuesController : ControllerBase
    {
        private readonly SomeSetting _someSetting;

        public ValuesController(SomeSetting someSetting)
        {
            _someSetting = someSetting;
        }
		
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { 
                "value1",
                "value2",
                _someSetting.Value
            };
        }
    }
```
5. In the `ConfigureServices()` method in your `Startup.cs` add the following to scan and inject settings types
```C#
    public void ConfigureServices(IServiceCollection services)
    {
        // Other services go here
		
        var assemblies = new[] {typeof(Startup).Assembly}; // assemblies to scan for settings
        services.AddConfigurationSettingsFrom(assemblies); // Wire it up
    }
```

See the sample project more advanced use cases like custom naming, complex types and arrays.

---

Feel free to contribute and raise issues as you see fit :)

- Creator: Dasith Wijesiriwardena (http://dasith.me)