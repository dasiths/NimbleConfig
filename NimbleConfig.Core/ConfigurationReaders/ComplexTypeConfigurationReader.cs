using System;
using Microsoft.Extensions.Configuration;
using NimbleConfig.Core.Configuration;

namespace NimbleConfig.Core.ConfigurationReaders
{
    public class ComplexTypeConfigurationReader : IConfigurationReader
    {
        public object Read(IConfiguration configuration, Type complexType, IKeyName keyName)
        {
            var key = keyName.QualifiedKeyName;
            return configuration.GetSection(key).Get(complexType);
        }
    }
}