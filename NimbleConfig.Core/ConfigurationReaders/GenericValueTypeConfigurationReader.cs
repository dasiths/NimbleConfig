using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using NimbleConfig.Core.Extensions;

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
