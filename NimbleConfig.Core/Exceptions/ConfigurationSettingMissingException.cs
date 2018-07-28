using System;
using NimbleConfig.Core.Configuration;

namespace NimbleConfig.Core.Exceptions
{
    public class ConfigurationSettingMissingException: Exception
    {
        public ConfigurationSettingMissingException(Type configType, IKeyName keyName): 
            base($"{configType.Name} couldn't be read from configuration using key: {keyName.QualifiedKeyName}")
        {
            
        }
    }
}
