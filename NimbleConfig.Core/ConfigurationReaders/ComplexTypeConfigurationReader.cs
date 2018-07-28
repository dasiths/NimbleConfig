using System;
using Microsoft.Extensions.Configuration;
using NimbleConfig.Core.Configuration;
using NimbleConfig.Core.Exceptions;
using NimbleConfig.Core.Logging;
using NimbleConfig.Core.Options;

namespace NimbleConfig.Core.ConfigurationReaders
{
    public class ComplexTypeConfigurationReader : IConfigurationReader
    {
        public object Read(IConfiguration configuration, Type complexType, IKeyName keyName, MissingConfigurationStratergy configurationStratergy)
        {
            var key = keyName.QualifiedKeyName;

            // return null if no section exists
            if (!configuration.GetSection(key).Exists())
            {
                StaticLoggingHelper.Warning($"No configuration for '{key}' was found.");

                if (configurationStratergy == MissingConfigurationStratergy.ThrowException)
                {
                    throw new ConfigurationSettingMissingException(complexType, keyName);
                }

                return null;
            }
            else
            {
                return configuration.GetSection(key).Get(complexType);
            }
        }
    }
}