using System;
using Microsoft.Extensions.Configuration;
using NimbleConfig.Core.Extensions;

namespace NimbleConfig.Core.ConfigurationReaders
{
    internal class ValueTypeConfigurationReader : IConfigurationReader
    {
        public object Read(IConfiguration configuration, Type configType, string key)
        {
            var valueType = configType.GetGenericTypeOfConfigurationSetting();

            return valueType.IsArray ? 
                configuration.GetSection(key).Get(valueType) : 
                configuration[key];
        }
    }
}
