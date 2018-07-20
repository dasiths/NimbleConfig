using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NimbleConfig.Core;

namespace NimbleConfig.Sample
{
    public class VeryStrangeType:ConfigurationSetting<int>
    {
        public override void SetValue(string configValue)
        {
            int number = 0;

            if (configValue == "use default")
            {
                number = 100;
            }

            Value = number;
        }
    }
}
