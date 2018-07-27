using NimbleConfig.Core.Configuration;

namespace NimbleConfig.Core.Tests.Settings
{
    public class SomeUnresolvedComplexSetting : IComplexConfigurationSetting
    {
        public string SomeProperty { get; set; }
    }
}