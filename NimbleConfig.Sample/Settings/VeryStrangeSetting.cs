using System;
using NimbleConfig.Core.Configuration;

namespace NimbleConfig.Sample.Settings
{
    public class VeryStrangeSetting:ConfigurationSetting<int>
    {
        public override void SetValue(object rawConfigValue, Func<object, object> valueParserFunc)
        {
            var useDefaultValue = rawConfigValue.ToString() == "default";
            var number = useDefaultValue ? 100 : (int)valueParserFunc(rawConfigValue);

            Value = number;
        }
    }
}
