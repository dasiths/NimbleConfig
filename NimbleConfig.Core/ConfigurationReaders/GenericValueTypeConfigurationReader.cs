using System;
using Microsoft.Extensions.Configuration;
using NimbleConfig.Core.Configuration;

namespace NimbleConfig.Core.ConfigurationReaders
{
    public class GenericValueTypeConfigurationReader : IConfigurationReader
    {
        public object Read(IConfiguration configuration, Type configType, IKeyName keyName)
        {
            var key = keyName.QualifiedKeyName;
            return configuration[key];
        }
    }
}
