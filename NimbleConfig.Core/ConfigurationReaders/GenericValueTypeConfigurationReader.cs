using System;
using Microsoft.Extensions.Configuration;

namespace NimbleConfig.Core.ConfigurationReaders
{
    internal class GenericValueTypeConfigurationReader : IConfigurationReader
    {
        public object Read(IConfiguration configuration, Type configType, string key)
        {
            return configuration[key];
        }
    }
}
