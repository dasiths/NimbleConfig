using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NimbleConfig.Samples.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Step 1. Construct the IConfiguration
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var configuration = (IConfiguration)builder.Build();

            // Step 2. Configure your DI container
            // See SetupConfigurationDependencies() method on how to register your config settings
            var serviceProvider = configuration.SetupConfigurationDependencies(new [] {Assembly.GetEntryAssembly()});

            // Step 3. Use it.
            // Get the setting from DI Container
            var setting = serviceProvider.GetRequiredService<SomeSetting>();

            Console.WriteLine($"Value of {nameof(SomeSetting)} is '{setting.Value}'");
            Console.ReadLine();
        }
    }
}
