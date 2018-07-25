using NimbleConfig.Core.Configuration;

namespace NimbleConfig.Core.Tests.Settings
{
    public class SomeComplexSetting : IComplexConfigurationSetting
    {
        public string SomeProperty { get; set; }
    }
}