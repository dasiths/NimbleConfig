using System;
using Microsoft.Extensions.Configuration;
using NimbleConfig.Core.Configuration;
using NimbleConfig.Core.Logging;

namespace NimbleConfig.Core.ConfigurationReaders
{
    public class ComplexTypeConfigurationReader : IConfigurationReader
    {
        public object Read(IConfiguration configuration, Type complexType, IKeyName keyName)
        {
            var key = keyName.QualifiedKeyName;

            // return null if no section exists
            if (!configuration.GetSection(key).Exists())
            {
                StaticLoggingHelper.Warning($"No configuration for '{key}' was found.");
                return null;
            }
            else
            {
                return configuration.GetSection(key).Get(complexType);
            }
        }
    }
}