using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using NimbleConfig.Core.Extensions;

namespace NimbleConfig.Core.ConfigurationReaders
{
    internal class ValueTypeConfigurationReader : IConfigurationReader
    {
        public object Read(IConfiguration configuration, Type configType, string key)
        {
            var valueType = configType.GetGenericTypeOfConfigurationSetting();

            if (valueType.IsValueType)
            {
                return configuration[key];
            }
            else
            {
                if (valueType.IsArray && valueType.HasElementType && !valueType.GetElementType().IsValueType)
                {
                    return configuration.GetSection(key).GetChildren()
                        .Select(config => config.Get(valueType.GetElementType()))
                        .ToArray();
                }

                return configuration.GetSection(key).Get(valueType);
            } 
        }
    }
}
