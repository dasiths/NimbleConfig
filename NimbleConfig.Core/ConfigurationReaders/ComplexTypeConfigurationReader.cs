using System;
using Microsoft.Extensions.Configuration;

namespace NimbleConfig.Core.ConfigurationReaders
{
    public class ComplexTypeConfigurationReader : IConfigurationReader
    {
        public object Read(IConfiguration configuration, Type complexType, string key)
        {
            return configuration.GetSection(key).Get(complexType);
        }
    }
}