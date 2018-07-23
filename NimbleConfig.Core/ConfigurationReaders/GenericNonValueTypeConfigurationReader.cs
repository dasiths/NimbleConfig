using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using NimbleConfig.Core.Configuration;
using NimbleConfig.Core.Extensions;

namespace NimbleConfig.Core.ConfigurationReaders
{
    public class GenericNonValueTypeConfigurationReader: IConfigurationReader
    {
        public object Read(IConfiguration configuration, Type configType, IKeyName keyName)
        {
            var valueType = configType.GetGenericTypeOfConfigurationSetting();
            var elementType = valueType.GetElementType();
            var key = keyName.QualifiedKeyName;

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
