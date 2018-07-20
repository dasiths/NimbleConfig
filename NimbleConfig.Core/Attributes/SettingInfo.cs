using System;
using System.Collections.Generic;
using System.Text;

namespace NimbleConfig.Core.Attributes
{
    public class SettingInfo : Attribute
    {
        public string Key { get; set; }
    }
}
