using NimbleConfig.Core.Configuration;
using NimbleConfig.Core.Options;
using NimbleConfig.Core.Tests.Settings;
using NimbleConfig.Core.Tests.Setup;
using Shouldly;
using Xunit;

namespace NimbleConfig.Core.Tests
{
    public class CustomNamingTests
    {
        [Fact]
        public void CanResolveSettingWithCustomName()
        {
            var configurationFactory = ConfigurationFactoryCreator.Create(new ConfigurationOptions()
            {
                CustomKeyName = (type, name) =>
                {
                    if (type == typeof(SomeDecimalSetting))
                    {
                        return new KeyName("SomeSection:", name.QualifiedKeyName);
                    }

                    return name;
                }
            });

            SomeDecimalSetting config = configurationFactory.CreateConfigurationSetting(typeof(SomeDecimalSetting));
            config.Value.ShouldBeOfType<decimal>();
            config.Value.Equals(decimal.Parse("0.12345")).ShouldBeTrue();
        }

        [Fact]
        public void CanResolveSettingWithWithAttribute()
        {
            var configurationFactory = ConfigurationFactoryCreator.Create(new ConfigurationOptions()
            {
                CustomKeyName = (type, name) =>
                {
                    if (type == typeof(SomeDecimalSettingWithAttribute))
                    {
                        name.QualifiedKeyName.ShouldBe("SomeDecimalSetting");
                        return new KeyName("SomeSection:", name.QualifiedKeyName);
                    }

                    return name;
                }
            });

            SomeDecimalSettingWithAttribute config = configurationFactory.CreateConfigurationSetting(typeof(SomeDecimalSettingWithAttribute));
            config.Value.ShouldBeOfType<decimal>();
            config.Value.Equals(decimal.Parse("0.12345")).ShouldBeTrue();
        }
    }
}