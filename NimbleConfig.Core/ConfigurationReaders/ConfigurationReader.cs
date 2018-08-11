using System;
using Microsoft.Extensions.Configuration;
using NimbleConfig.Core.Configuration;

namespace NimbleConfig.Core.ConfigurationReaders
{
    public abstract class ConfigurationReader: IConfigurationReader
    {
        public abstract object Read(IConfiguration configuration, Type configType, IKeyName keyName);

        public bool ValueExists(IConfiguration configuration, Type configType, IKeyName keyName)
        {
            var key = keyName.QualifiedKeyName;
            return configuration.GetSection(key).Exists();
        }
    }
}
