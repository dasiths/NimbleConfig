using NimbleConfig.Core.Attributes;
using NimbleConfig.Core.Configuration;

namespace NimbleConfig.Core.Tests.Settings
{
    [SettingInfo (Key = "SomeDecimalSetting")]
    public class SomeDecimalSettingWithAttribute : ConfigurationSetting<decimal>
    {
    }
}