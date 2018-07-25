using NimbleConfig.Core.Factory;
using NimbleConfig.Core.Tests.Settings;
using NimbleConfig.Core.Tests.Setup;
using Shouldly;
using Xunit;

namespace NimbleConfig.Core.Tests
{
    public class ComplexTypeSettingTests
    {
        private readonly ConfigurationFactory _configurationFactory;

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
    }
}
