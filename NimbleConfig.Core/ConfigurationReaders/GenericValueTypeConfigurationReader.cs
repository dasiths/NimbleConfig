using System;
using Microsoft.Extensions.Configuration;
using NimbleConfig.Core.Configuration;
using NimbleConfig.Core.Exceptions;
using NimbleConfig.Core.Extensions;
using NimbleConfig.Core.Logging;
using NimbleConfig.Core.Options;

namespace NimbleConfig.Core.ConfigurationReaders
{
    public class GenericValueTypeConfigurationReader : IConfigurationReader
    {
        public object Read(IConfiguration configuration, Type configType, IKeyName keyName, MissingConfigurationStratergy configurationStratergy)
        {
            var key = keyName.QualifiedKeyName;
            var valueType = configType.GetGenericTypeOfConfigurationSetting();

            // Making sure the section exists
            if (!configuration.GetSection(key).Exists())
            {
                // Return default value if no section exists
                StaticLoggingHelper.Warning($"No configuration for '{key}' was found.");

                if (configurationStratergy == MissingConfigurationStratergy.ThrowException)
                {
                    throw new ConfigurationSettingMissingException(configType, keyName);
                }

                return GetDefault(valueType);
            }

            return configuration.GetSection(key).Get(valueType);
        }

        public object GetDefault(Type t)
        {
            return this.GetType().GetMethod(nameof(GetDefaultGeneric))?.MakeGenericMethod(t).Invoke(this, null);
        }

        public T GetDefaultGeneric<T>()
        {
            return default(T);
        }
    }
}
