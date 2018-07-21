using NimbleConfig.Core.Configuration;
using NimbleConfig.Core.Parsers;

namespace NimbleConfig.Sample.Settings
{
    public class VeryStrangeSetting:ConfigurationSetting<int>
    {
        public override void SetValue(object configValue, IParser parser)
        {
            var useDefaultValue = configValue.ToString() == "default";
            var number = useDefaultValue ? 100 : (int)parser.Parse(typeof(int), configValue);

            Value = number;
        }
    }
}
