using NimbleConfig.Core.Exceptions;
using NimbleConfig.Core.Factory;
using NimbleConfig.Core.Options;
using NimbleConfig.Core.Tests.Settings;
using NimbleConfig.Core.Tests.Setup;
using Xunit;
using Shouldly;

namespace NimbleConfig.Core.Tests
{
    public class GenericTypeSettingTests
    {
        private IConfigurationFactory _configurationFactory;

        public GenericTypeSettingTests()
        {
            _configurationFactory = ConfigurationFactoryCreator.Create();
        }

        [Fact]
        public void CanResolveStringSetting()
        {
            SomeSetting config = _configurationFactory.CreateConfigurationSetting(typeof(SomeSetting));
            config.Value.ShouldBeOfType<string>();
            config.Value.ShouldBe("SomeValue");
        }

        [Fact]
        public void CanResolveIntSetting()
        {
            SomeIntSetting config = _configurationFactory.CreateConfigurationSetting(typeof(SomeIntSetting));
            config.Value.ShouldBeOfType<int>();
            config.Value.ShouldBe(10);
        }

        [Fact]
        public void CanResolveBoolSetting()
        {
            SomeBoolSetting config = _configurationFactory.CreateConfigurationSetting(typeof(SomeBoolSetting));
            config.Value.ShouldBeOfType<bool>();
            config.Value.ShouldBe(true);
        }

        [Fact]
        public void CanResolveDecimalSetting()
        {
            SomeDecimalSetting config = _configurationFactory.CreateConfigurationSetting(typeof(SomeDecimalSetting));
            config.Value.ShouldBeOfType<decimal>();
            config.Value.Equals(decimal.Parse("1.2345")).ShouldBeTrue();
        }

        [Fact]
        public void CanResolveArraySetting()
        {
            SomeArraySetting config = _configurationFactory.CreateConfigurationSetting(typeof(SomeArraySetting));
            config.Value.ShouldBeOfType<int[]>();
            config.Value.Length.ShouldBe(4);
            config.Value[0].ShouldBe(1);
        }

        [Fact]
        public void CanResolveEnumSetting()
        {
            SomeEnumSetting config = _configurationFactory.CreateConfigurationSetting(typeof(SomeEnumSetting));
            config.Value.ShouldBeOfType<SomeEnum>();
            config.Value.ShouldBe(SomeEnum.One);
        }

        [Fact]
        public void CanResolveEnumArraySetting()
        {
            SomeEnumArraySetting config = _configurationFactory.CreateConfigurationSetting(typeof(SomeEnumArraySetting));
            config.Value.ShouldBeOfType<SomeEnum[]>();
            config.Value.Length.ShouldBe(3);
            config.Value[0].ShouldBe(SomeEnum.One);
        }

        [Fact]
        public void CanResolveComplexArraySetting()
        {
            SomeComplexArraySetting config = _configurationFactory.CreateConfigurationSetting(typeof(SomeComplexArraySetting));
            config.Value.ShouldBeOfType<ComplexType[]>();
            config.Value.Length.ShouldBe(2);
            config.Value[0].Key.ShouldBe("Key1");
            config.Value[0].Value.ShouldBe("Value1");
        }

        [Fact]
        public void CantResolveMissingStringSetting()
        {
            SomeUnresolvedSetting config = _configurationFactory.CreateConfigurationSetting(typeof(SomeUnresolvedSetting));
            string.IsNullOrEmpty(config.Value).ShouldBeTrue();
        }

        [Fact]
        public void MissingStringSettingThrowsException()
        {
            var options = ConfigurationOptions.Create();
            options.MissingConfigurationStratergy = MissingConfigurationStratergy.ThrowException;

            _configurationFactory = ConfigurationFactoryCreator.Create(options);

            Should.Throw<ConfigurationSettingMissingException>(() =>
            {
                _configurationFactory.CreateConfigurationSetting(typeof(SomeUnresolvedSetting));
            });
        }


    }
}
