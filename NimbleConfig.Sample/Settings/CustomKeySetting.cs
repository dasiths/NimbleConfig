using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NimbleConfig.Core.Attributes;
using NimbleConfig.Core.Configuration;

namespace NimbleConfig.Sample.Settings
{
    [SettingInfo(Key = "SomeCustomNamedSetting")]
    public class CustomKeySetting: ConfigurationSetting<decimal>
    {
    }
}
