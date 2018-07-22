using System;
using Microsoft.Extensions.Configuration;

namespace NimbleConfig.Core.ConfigurationReaders
{
    public class GenericValueTypeConfigurationReader : IConfigurationReader
    {
        public object Read(IConfiguration configuration, Type configType, string key)
        {
            return configuration[key];
        }
    }
}
