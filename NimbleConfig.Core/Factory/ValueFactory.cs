using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using NimbleConfig.Core.Attributes;
using NimbleConfig.Core.Configuration;
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
            
            // Resolve the key and prefix names
            var key = KeyNameResolver.ResolveKeyName(configType, _configurationOptions);

            // Read configuration value
            var reader = ConfigurationReaderResolver.ResolveReader(configType, _configurationOptions);
            var value = reader.Read(_configuration, configType, key);

            // Pick parser
            var parser = ParserResolver.ResolveParser(configType, _configurationOptions);

            // Set the value
            var configSetting = ConstructConfigurationValue(configType, value, parser);
            return configSetting;

        }

        public dynamic ConstructConfigurationValue(Type configType, object value, IParser parser)
        {
            var settingType = configType.GetConfigurationSettingType();

            switch (settingType)
            {
                case ConfigurationSettingType.GenericValueType:
                    return CreateValueType(configType, value, parser);
                case ConfigurationSettingType.ComplexType:
                    return CreateComplexType(configType, value, parser);
                case ConfigurationSettingType.None:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return null;
        }

        private static dynamic CreateComplexType(Type configType, object value, IParser parser)
        {
            value = parser == null ? value : parser.Parse(configType, value);
            return value;
        }

        private static dynamic CreateValueType(Type configType, object value, IParser parser)
        {
            dynamic config = Activator.CreateInstance(configType);
            var genericType = configType.GetGenericTypeOfConfigurationSetting();
            object ParserFunc(object rawValue) => parser.Parse(genericType, rawValue);
            config.SetValue(value, (Func<object, object>) ParserFunc);
            return config;
        }
    }
}
