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

            // Construct the IConfiguration
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var configuration = (IConfiguration)builder.Build();

            // Configure your DI Container
            var collection = new ServiceCollection();
            collection.AddSingleton(configuration);
            collection.AddConfigurationSettingsFrom(new[] { Assembly.GetExecutingAssembly() });
            var serviceProvider = collection.BuildServiceProvider();

            // Get the setting from DI Container
            var setting = serviceProvider.GetRequiredService<SomeSetting>();

            Console.WriteLine($"Value of {nameof(SomeSetting)} is '{setting.Value}'");
            Console.ReadLine();
        }
    }
}
