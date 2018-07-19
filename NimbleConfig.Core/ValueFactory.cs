using System;
using Microsoft.Extensions.Configuration;

namespace NimbleConfig.Core
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
            var valueType = configType.BaseType.GetGenericArguments()[0];

            var value = _configuration[configType.Name];

            var convertedValue = Convert.ChangeType(value, valueType);

            var property = configType.GetProperty("Value");
            property.SetValue(config, convertedValue, null);

            return config;
        }
    }
}
