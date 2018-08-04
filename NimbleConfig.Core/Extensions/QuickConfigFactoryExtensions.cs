using System;
using Microsoft.Extensions.Configuration;
using NimbleConfig.Core.Configuration;
using NimbleConfig.Core.Factory;
using NimbleConfig.Core.Options;

namespace NimbleConfig.Core.Extensions
{
    public static class QuickConfigFactoryExtensions
    {
        public static TValue QuickReadSettingValue<TConfig, TValue>(this IConfiguration configuration, IConfigurationOptions options = null) where TConfig : ConfigurationSetting<TValue>
        {
            return QuickConfigFactory.GetSetting<TConfig>(configuration, options).Value;
        }

        public static TConfig QuickReadSetting<TConfig>(this IConfiguration configuration, IConfigurationOptions options = null)
        {
            return (TConfig)configuration.QuickReadSetting(typeof(TConfig), options);
        }

        public static object QuickReadSetting(this IConfiguration configuration, Type configType, IConfigurationOptions options = null)
        {
            if (configType.GetConfigurationSettingType() != ConfigurationSettingType.None)
            {
                return QuickConfigFactory.GetSetting(configType, configuration, options);
            }

            throw new ArgumentException("The type provided is not a known configuration setting type", nameof(configType));
        }
    }
}
