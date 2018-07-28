﻿using NimbleConfig.Core.Exceptions;
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
        private ConfigurationFactory _configurationFactory;

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
            SomeUnresolvedComplexSetting config = _configurationFactory.CreateConfigurationSetting(typeof(SomeUnresolvedComplexSetting));
            config.ShouldBeNull();
        }

        [Fact]
        public void MissingStringThrowsException()
        {
            _configurationFactory = ConfigurationFactoryCreator.Create(MissingConfigurationStratergy.ThrowException);

            Should.Throw<ConfigurationSettingMissingException>(() =>
            {
                _configurationFactory.CreateConfigurationSetting(typeof(SomeUnresolvedComplexSetting));
            });
        }
    }
}
