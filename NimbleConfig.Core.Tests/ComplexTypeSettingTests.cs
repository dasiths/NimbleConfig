using NimbleConfig.Core.Exceptions;
using NimbleConfig.Core.Factory;
using NimbleConfig.Core.Options;
using NimbleConfig.Core.Tests.Settings;
using NimbleConfig.Core.Tests.Setup;
using Shouldly;
using Xunit;

namespace NimbleConfig.Core.Tests
{
    public class ComplexTypeSettingTests
    {
        private IConfigurationFactory _configurationFactory;

        public ComplexTypeSettingTests()
        {
            _configurationFactory = ConfigurationFactoryCreator.Create();
        }

        [Fact]
        public void CanResolveStringSetting()
        {
            SomeComplexSetting config = _configurationFactory.CreateConfigurationSetting(typeof(SomeComplexSetting));
            config.SomeProperty.ShouldBe("SomeValue");
        }

        [Fact]
        public void CantResolveMissingComplexSetting()
        {
            SomeUnresolvedComplexSetting config =
                _configurationFactory.CreateConfigurationSetting(typeof(SomeUnresolvedComplexSetting));
            config.ShouldBeNull();
        }

        [Fact]
        public void MissingComplexSettingThrowsException()
        {
            var options = ConfigurationOptions.Create();
            options.MissingConfigurationStratergy = MissingConfigurationStratergy.ThrowException;

            _configurationFactory = ConfigurationFactoryCreator.Create(options);

            Should.Throw<ConfigurationSettingMissingException>(() =>
            {
                _configurationFactory.CreateConfigurationSetting(typeof(SomeUnresolvedComplexSetting));
            });
        }
    }
}
