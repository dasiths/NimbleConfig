using NimbleConfig.Core.Configuration;
using NimbleConfig.Core.Extensions;
using NimbleConfig.Core.Factory;
using NimbleConfig.Core.Options;
using NimbleConfig.Core.Tests.Settings;
using NimbleConfig.Core.Tests.Setup;
using Shouldly;
using Xunit;

namespace NimbleConfig.Core.Tests
{
    public class ConfigurationOptionsTests
    {
        [Fact]
        public void CanResolveSettingWithCustomName()
        {
            var options = ConfigurationOptionFactory.Create();
            options.WithNamingScheme((type, name) =>
            {
                if (type == typeof(SomeDecimalSetting))
                {
                    return new KeyName("SomeSection:", name.QualifiedKeyName);
                }

                return name;
            });

            var configurationFactory = ConfigurationFactoryCreator.Create(options);

            SomeDecimalSetting config = configurationFactory.CreateConfigurationSetting(typeof(SomeDecimalSetting));
            config.Value.ShouldBeOfType<decimal>();
            config.Value.Equals(decimal.Parse("0.12345")).ShouldBeTrue();
        }

        [Fact]
        public void CanResolveSettingWithNameAttribute()
        {
            var options = ConfigurationOptionFactory.Create();
            options.WithNamingScheme((type, name) =>
            {
                if (type == typeof(SomeDecimalSettingWithAttribute))
                {
                    name.QualifiedKeyName.ShouldBe("SomeDecimalSetting");
                    return new KeyName("SomeSection:", name.QualifiedKeyName);
                }

                return name;
            });

            var configurationFactory = ConfigurationFactoryCreator.Create(options);

            SomeDecimalSettingWithAttribute config = configurationFactory.CreateConfigurationSetting(typeof(SomeDecimalSettingWithAttribute));
            config.Value.ShouldBeOfType<decimal>();
            config.Value.Equals(decimal.Parse("0.12345")).ShouldBeTrue();
        }

        [Fact]
        public void CanResolveUsingCustomReader()
        {
            var count = 0;

            var options = ConfigurationOptionFactory.Create();
            options.WithReader((type, reader) =>
            {
                type.ShouldBe(typeof(SomeDecimalSetting));
                reader.ShouldNotBeNull();
                count.ShouldBe(0);
                count++;
                return reader;
            });
            
            // Chaining multiple functions
            options.WithReader((type, reader) =>
            {
                type.ShouldBe(typeof(SomeDecimalSetting));
                reader.ShouldNotBeNull();
                count.ShouldBe(1);
                return reader;
            });

            var configurationFactory = ConfigurationFactoryCreator.Create(options);

            SomeDecimalSetting config = configurationFactory.CreateConfigurationSetting(typeof(SomeDecimalSetting));
            config.Value.ShouldBeOfType<decimal>();
            config.Value.Equals(decimal.Parse("1.2345")).ShouldBeTrue();
        }

        [Fact]
        public void CanResolveUsingCustomParser()
        {
            var count = 0;

            var options = ConfigurationOptionFactory.Create();
            options.WithParser((type, parser) =>
            {
                type.ShouldBe(typeof(SomeDecimalSetting));
                parser.ShouldNotBeNull();
                count.ShouldBe(0);
                count++;
                return parser;
            });

            // Chaining multiple functions
            options.WithParser((type, parser) =>
            {
                type.ShouldBe(typeof(SomeDecimalSetting));
                parser.ShouldNotBeNull();
                count.ShouldBe(1);
                return parser;
            });

            var configurationFactory = ConfigurationFactoryCreator.Create(options);

            SomeDecimalSetting config = configurationFactory.CreateConfigurationSetting(typeof(SomeDecimalSetting));
            config.Value.ShouldBeOfType<decimal>();
            config.Value.Equals(decimal.Parse("1.2345")).ShouldBeTrue();
        }

        [Fact]
        public void CanResolveUsingCustomConstructors()
        {
            var count = 0;

            var options = ConfigurationOptionFactory.Create();
            options.WithConstructor((type, constructor) =>
            {
                type.ShouldBe(typeof(SomeDecimalSetting));
                constructor.ShouldNotBeNull();
                count.ShouldBe(0);
                count++;
                return constructor;
            });

            // Chaining multiple functions
            options.WithConstructor((type, constructor) =>
            {
                type.ShouldBe(typeof(SomeDecimalSetting));
                constructor.ShouldNotBeNull();
                count.ShouldBe(1);
                return constructor;
            });

            var configurationFactory = ConfigurationFactoryCreator.Create(options);

            SomeDecimalSetting config = configurationFactory.CreateConfigurationSetting(typeof(SomeDecimalSetting));
            config.Value.ShouldBeOfType<decimal>();
            config.Value.Equals(decimal.Parse("1.2345")).ShouldBeTrue();
        }
    }
}