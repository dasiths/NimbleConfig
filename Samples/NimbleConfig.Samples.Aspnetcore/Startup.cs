using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NimbleConfig.Core.Extensions;
using NimbleConfig.Core.Options;
using NimbleConfig.DependencyInjection.Aspnetcore;
using NimbleConfig.Samples.Aspnetcore.Settings;

namespace NimbleConfig.Samples.Aspnetcore
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();

            // Use this method *ONLY* if you need to read a setting prior to setting up your IOC container
            // This quick reads create a factory per use so use it as a last resort
            var dirtySetting = Configuration.QuickReadSetting<SomeSetting>();
            Console.WriteLine($"Dirty read of {typeof(SomeSetting).Name}: {dirtySetting.Value}");
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(Configuration); // This is needed if you want to use the same config instance built in the constructor,
            // Else it will use the default one from the webhost builder
            // https://stackoverflow.com/questions/46574521/is-services-addsingletoniconfiguration-really-needed-in-net-core-2-api

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // Throw an exception if missing settings are being used
            var configOptions = ConfigurationOptions.Create()
                .WithExceptionForMissingSettings();

            // Wire up our settings
            services.AddConfigurationSettings()
                .WithSingletonInstances()
                .UsingOptionsIn(configOptions)
                .UsingLogger<MyConfigLogger>()
                .AndBuild();

            // Or use the built in console logger like this: 
            //.UsingLogger<ConsoleConfigLogger>()

            // Note: You can use .WithScopedInstances() for per request inatance of settings
            // This works great withg the 'reloadOnChange' option for appsettings.json files

            services.AddLogging();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseMvc();
        }
    }


}
