using NimbleConfig.Core.Options;

namespace NimbleConfig.Core.Factory
{
    public static class ConfigurationOptionFactory
    {
        public static IConfigurationOptions Create()
        {
            return new ConfigurationOptions();
        }
    }
}
