using System;
using NimbleConfig.Core.Configuration;

namespace NimbleConfig.Samples.Aspnetcore.Settings
{
    public class VeryStrangeSetting:ConfigurationSetting<int>
    {
        public override void SetValue(int value)
        {
            var useDefaultValue = value == 0;
            var number = useDefaultValue ? 100 : value;

            Value = number;
        }
    }
}
