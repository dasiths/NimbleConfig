using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using NimbleConfig.Core.Attributes;
using NimbleConfig.Core.Extensions;
using NimbleConfig.Core.Options;

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
            object value = null;

            var genericType = configType.GetGenericTypeOfConfigurationSetting();

            value = genericType.IsValueType ? _configuration.ReadValueType(key) : _configuration.ReadComplexType(genericType, key);

            config.SetValue(value);
            return config;
        }

        public dynamic CreateComplexConfigurationSetting(Type configType)
        {
            var key = KeyNameResolver.GetKeyName(configType, _configurationOptions);
            var value = _configuration.ReadComplexType(configType, key);
            return value;
        }
    }
}
