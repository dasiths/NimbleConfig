using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NimbleConfig.Core.Logging;
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

            /* Note:
             * reloadOnChange: true works only if you have a
             * lifetime scope for your config settings
             */

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // Wire up our settings
            services.AddConfigurationSettings();

            /* Note: You can use AddConfigurationSettings(settingLifetime: ServiceLifetime.Scoped)
             * for per request settings that read the latest value from config
             */

            services.AddLogging();

            // Optional: Adding custom logger that implements IConfigLogger
            services.AddSingleton<IConfigLogger, CustomConfigConfigLogger>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseMvc();
        }
    }


}
