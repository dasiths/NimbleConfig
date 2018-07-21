using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using NimbleConfig.Core.Attributes;
using NimbleConfig.Core.Extensions;
using NimbleConfig.Core.Options;
using NimbleConfig.Core.Parsers;
using NimbleConfig.Core.Resolvers;

namespace NimbleConfig.Core.Factory
{
    public class ValueFactory
    {
        private readonly IConfiguration _configuration;
        private readonly ConfigurationOptions _configurationOptions;

        public ValueFactory(IConfiguration configuration,
            ConfigurationOptions configurationOptions)
        {
            _configuration = configuration;
            _configurationOptions = configurationOptions;
        }

        public dynamic CreateConfigurationSetting(Type configType)
        {
            // Todo: handle missing config settings

            dynamic config = Activator.CreateInstance(configType);
            var key = KeyNameResolver.GetKeyName(configType, _configurationOptions);

            // Get the configuration type
            var genericType = configType.GetGenericTypeOfConfigurationSetting();

            // Read configuration value
            var value = genericType.IsValueType ? 
                _configuration.ReadValueType(key) : 
                _configuration.ReadComplexType(genericType, key);

            // Pick parser
            var parser = ParserResolver.GetParser(genericType, _configurationOptions);
            
            // Set the value
            config.SetValue(value, parser);
            return config;
        }

        public dynamic CreateComplexConfigurationSetting(Type configType)
        {
            var key = KeyNameResolver.GetKeyName(configType, _configurationOptions);

            // Read complex value from configuration
            var value = _configuration.ReadComplexType(configType, key);
            return value;
        }
    }
}
