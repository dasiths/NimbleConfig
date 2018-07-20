using NimbleConfig.Core.Configuration;

namespace NimbleConfig.Sample.Settings
{
    public class SomeComplexTypeSetting: IComplexConfigurationSetting
    {
        public string SomeProp { get; set; }
    }
}
