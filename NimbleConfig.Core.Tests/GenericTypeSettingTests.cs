using System.IO;
using Microsoft.Extensions.Configuration;
using NimbleConfig.Core.Factory;
using NimbleConfig.Core.Options;
using NimbleConfig.Core.Resolvers;
using NimbleConfig.Core.Tests.Settings;
using Xunit;
using Shouldly;

namespace NimbleConfig.Core.Tests
{
    public class GenericTypeSettingTests
    {
        private readonly ConfigurationFactory _configurationFactory;

        public GenericTypeSettingTests()
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

            _configurationFactory = new ConfigurationFactory(configuration,
                                                            options,
                                                            keynameResolver,
                                                            configResolver,
                                                            parserResolver,
                                                            constructorResolver);
        }

        [Fact]
        public void PassingTest()
        {
            SomeSetting config = _configurationFactory.CreateConfigurationSetting(typeof(SomeSetting));
            config.Value.ShouldBeOfType<string>();
            config.Value.ShouldBe("SomeValue");
        }

    }
}
