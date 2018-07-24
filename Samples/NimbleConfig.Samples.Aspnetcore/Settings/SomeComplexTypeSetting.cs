using NimbleConfig.Core.Configuration;

namespace NimbleConfig.Samples.Aspnetcore.Settings
{
    public class SomeComplexTypeSetting: IComplexConfigurationSetting
    {
        public string SomeProp { get; set; }
    }
}
