using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NimbleConfig.Core;

namespace NimbleConfig.Sample
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
