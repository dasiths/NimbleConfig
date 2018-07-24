using NimbleConfig.Core.Attributes;
using NimbleConfig.Core.Configuration;

namespace NimbleConfig.Samples.Aspnetcore.Settings
{
    [SettingInfo(Key = "Some:CustomNamedSetting")]
    public class CustomKeySetting: ConfigurationSetting<decimal>
    {
    }
}
