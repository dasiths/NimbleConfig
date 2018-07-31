using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NimbleConfig.Core.Extensions;
using NimbleConfig.Core.Factory;
using NimbleConfig.Core.Logging;
using NimbleConfig.Core.Options;
using NimbleConfig.DependencyInjection.Aspnetcore;

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
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // Ignore missing settings
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
