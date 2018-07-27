using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using NimbleConfig.Core.Configuration;
using NimbleConfig.Core.Extensions;
using NimbleConfig.Core.Logging;

namespace NimbleConfig.Core.ConfigurationReaders
{
    public class GenericNonValueTypeConfigurationReader: IConfigurationReader
    {
        public object Read(IConfiguration configuration, Type configType, IKeyName keyName)
        {
            var valueType = configType.GetGenericTypeOfConfigurationSetting();
            var elementType = valueType.GetElementType();
            var key = keyName.QualifiedKeyName;

            // Making sure the section exists
            if (!configuration.GetSection(key).Exists())
            {
                // Return null if no section exists
                StaticLoggingHelper.Warning($"No configuration for '{key}' was found.");
                return null;
            }

            // Handle Complex Arrays
            if (valueType.IsArray && elementType != null && !elementType.IsValueType)
            {
                return configuration.GetSection(key).GetChildren()
                    .Select(config => config.Get(elementType))
                    .ToArray();
            }

            return configuration.GetSection(key).Get(valueType);
        }
    }
}
