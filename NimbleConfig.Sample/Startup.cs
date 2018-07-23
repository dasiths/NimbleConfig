﻿using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NimbleConfig.Core.Logging;
using NimbleConfig.Core.Options;
using NimbleConfig.DependencyInjection.Aspnetcore;

namespace NimbleConfig.Sample
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

            // Wire up our settings
            var assemblies = new[] { typeof(Startup).Assembly };
            services.AddConfigurationSettingsFrom(assemblies);

            services.AddLogging();

            // Optional: Adding custom logger that implements IConfigLogger
            services.AddSingleton<IConfigLogger, CustomConfigConfigLogger>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }


}
