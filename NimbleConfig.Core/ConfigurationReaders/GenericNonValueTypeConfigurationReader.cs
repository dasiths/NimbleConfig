using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using NimbleConfig.Core.Extensions;

namespace NimbleConfig.Core.ConfigurationReaders
{
    internal class GenericNonValueTypeConfigurationReader: IConfigurationReader
    {
        public object Read(IConfiguration configuration, Type configType, string key)
        {
            // Handle Array Types
            var valueType = configType.GetGenericTypeOfConfigurationSetting();
            var elementType = valueType.GetElementType();

            // Handle Is ArrayType
            if (valueType.IsArray && elementType != null && !elementType.IsValueType)
            {
                return configuration.GetSection(key).GetChildren()
                    .Select(config => config.Get(valueType.GetElementType()))
                    .ToArray();
            }

            return configuration.GetSection(key).Get(valueType);
        }
    }
}
