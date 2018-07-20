using NimbleConfig.Core.Configuration;

namespace NimbleConfig.Sample.Settings
{
    public class VeryStrangeSetting:ConfigurationSetting<int>
    {
        public override void SetValue(object configValue)
        {
            var useDefaultValue = configValue.ToString() == "default";
            var number = useDefaultValue ? 100 : int.Parse(configValue.ToString());

            Value = number;
        }
    }
}
