using System.IO;
using Microsoft.Extensions.Configuration;
using NimbleConfig.Core.Factory;
using NimbleConfig.Core.Options;
using NimbleConfig.Core.Resolvers;

namespace NimbleConfig.Core.Tests.Setup
{
    public class ConfigurationFactoryCreator
    {
        public static ConfigurationFactory Create()
        {
            // Construct the IConfiguration
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var configuration = (IConfiguration)builder.Build();

            // Configure the core pieces
            var keynameResolver = new KeyNameResolver();
            var parserResolver = new ParserResolver();
            var configResolver = new ConfigurationReaderResolver();
            var constructorResolver = new ValueConstructorResolver();

            var options = new ConfigurationOptions();

            var configuratinFactory = new ConfigurationFactory(configuration,
                options,
                keynameResolver,
                configResolver,
                parserResolver,
                constructorResolver);
            return configuratinFactory;
        }
    }
}