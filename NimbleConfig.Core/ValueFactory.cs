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
            var value = _configuration[configType.Name];
            config.SetValue(value);

            return config;
        }
    }
}
