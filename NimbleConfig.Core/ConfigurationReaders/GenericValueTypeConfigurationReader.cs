using System;
using Microsoft.Extensions.Configuration;
using NimbleConfig.Core.Configuration;
using NimbleConfig.Core.Extensions;

namespace NimbleConfig.Core.ConfigurationReaders
{
    public class GenericValueTypeConfigurationReader : IConfigurationReader
    {
        public object Read(IConfiguration configuration, Type configType, IKeyName keyName)
        {
            var key = keyName.QualifiedKeyName;
            var valueType = configType.GetGenericTypeOfConfigurationSetting();

            return configuration.GetSection(key).Get(valueType);
        }
    }
}
