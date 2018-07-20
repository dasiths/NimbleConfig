using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NimbleConfig.Core;

namespace NimbleConfig.Sample
{
    public class SomeComplexType: IComplexConfigurationSetting
    {
        public string SomeProp { get; set; }
    }
}
