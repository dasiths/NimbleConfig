using System;
using Microsoft.Extensions.Configuration;
using NimbleConfig.Core.Extensions;

namespace NimbleConfig.Core.Factory
{
    public class ValueFactory
    {
        private readonly IConfiguration _configuration;
        public ValueFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public dynamic CreateConfigurationSetting(Type configType)
        {
            dynamic config = Activator.CreateInstance(configType);

            var key = configType.Name;
            object value = null;

            var genericType = configType.GetGenericTypeOfConfigurationSetting();

            value = genericType.IsValueType ? _configuration.ReadValueType(key) : _configuration.ReadComplexType(genericType, key);

            config.SetValue(value);
            return config;
        }

        public dynamic CreateComplexConfigurationSetting(Type configType)
        {
            var key = configType.Name;
            var value = _configuration.ReadComplexType(configType, key);
            return value;
        }
    }
}
