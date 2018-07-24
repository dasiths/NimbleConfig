using NimbleConfig.Core.Configuration;

namespace NimbleConfig.Samples.Aspnetcore.Settings
{
    public class ComplexArraySetting: ConfigurationSetting<AComplexType[]>
    {
    }

    public class AComplexType
    {
        public string Key { get; set; }
    }
}
