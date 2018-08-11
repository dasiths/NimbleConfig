using System;
using Microsoft.Extensions.Configuration;
using NimbleConfig.Core.Configuration;
using NimbleConfig.Core.Extensions;
using NimbleConfig.Core.Logging;

namespace NimbleConfig.Core.ConfigurationReaders
{
    public class GenericValueTypeConfigurationReader : ConfigurationReader
    {
        public override object Read(IConfiguration configuration, Type configType, IKeyName keyName)
        {
            var key = keyName.QualifiedKeyName;
            var valueType = configType.GetGenericTypeOfConfigurationSetting();

            // Making sure the section exists
            if (!ValueExists(configuration, configType, keyName))
            {
                // Return default value if no section exists
                StaticLoggingHelper.Warning($"No configuration for '{key}' was found.");
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
