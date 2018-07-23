using System;
using Microsoft.Extensions.Configuration;
using NimbleConfig.Core.Configuration;
using NimbleConfig.Core.ConfigurationReaders;
using NimbleConfig.Core.Options;
using NimbleConfig.Core.Parsers;
using NimbleConfig.Core.Resolvers;
using NimbleConfig.Core.ValueConstructors;

namespace NimbleConfig.Core.Factory
{
    public class ConfigurationFactory
    {
        private readonly IResolver<IKeyName> _keyNameResolver;
        private readonly IResolver<IConfigurationReader> _configurationReaderResolver;
        private readonly IResolver<IParser> _parserResolver;
        private readonly IResolver<IValueConstructor> _valueConstructorResolver;

        private readonly IConfiguration _configuration;
        private readonly ConfigurationOptions _configurationOptions;

        public ConfigurationFactory(IConfiguration configuration,
            ConfigurationOptions configurationOptions, 
            IResolver<IKeyName> keyNameResolver, 
            IResolver<IConfigurationReader> configurationReaderResolver, 
            IResolver<IParser> parserResolver, 
            IResolver<IValueConstructor> valueConstructorResolver)
        {
            _configuration = configuration;
            _configurationOptions = configurationOptions;
            _keyNameResolver = keyNameResolver;
            _configurationReaderResolver = configurationReaderResolver;
            _parserResolver = parserResolver;
            _valueConstructorResolver = valueConstructorResolver;
        }

        public dynamic CreateConfigurationSetting(Type configType)
        {
            // Todo: handle missing config settings
            
            // Resolve the key and prefix names
            var keyName = _keyNameResolver.Resolve(configType, _configurationOptions);

            // Read configuration value
            var reader = _configurationReaderResolver.Resolve(configType, _configurationOptions);

            // Pick parser
            var parser = _parserResolver.Resolve(configType, _configurationOptions);

            // Pick constructor
            var valueConstructor = _valueConstructorResolver.Resolve(configType, _configurationOptions);

            // Set the value
            var configSetting = valueConstructor?.ConstructValue(_configuration, configType, keyName, reader, parser);
            return configSetting;

        }


    }
}
