using System;
using Microsoft.Extensions.Configuration;
using NimbleConfig.Core.Configuration;
using NimbleConfig.Core.ConfigurationReaders;
using NimbleConfig.Core.Extensions;
using NimbleConfig.Core.Logging;
using NimbleConfig.Core.Options;
using NimbleConfig.Core.Parsers;
using NimbleConfig.Core.Resolvers;
using NimbleConfig.Core.ValueConstructors;

namespace NimbleConfig.Core.Factory
{
    public class ConfigurationFactory : IConfigurationFactory
    {
        private readonly IResolver<IKeyName> _keyNameResolver;
        private readonly IResolver<IConfigurationReader> _configurationReaderResolver;
        private readonly IResolver<IParser> _parserResolver;
        private readonly IResolver<IValueConstructor> _valueConstructorResolver;

        private readonly IConfiguration _configuration;
        private readonly IConfigurationOptions _configurationOptions;

        public ConfigurationFactory(IConfiguration configuration,
            IConfigurationOptions configurationOptions,
            IResolver<IKeyName> keyNameResolver,
            IResolver<IConfigurationReader> configurationReaderResolver,
            IResolver<IParser> parserResolver,
            IResolver<IValueConstructor> valueConstructorResolver,
            IConfigLogger configLogger = null)
        {
            _configuration = configuration
                .EnsureNotNull(nameof(configuration));
            _configurationOptions = configurationOptions
                .EnsureNotNull(nameof(configurationOptions));
            _keyNameResolver = keyNameResolver
                .EnsureNotNull(nameof(keyNameResolver));
            _configurationReaderResolver = configurationReaderResolver
                .EnsureNotNull(nameof(configurationReaderResolver));
            _parserResolver = parserResolver
                .EnsureNotNull(nameof(parserResolver));
            _valueConstructorResolver = valueConstructorResolver
                .EnsureNotNull(nameof(valueConstructorResolver));

            StaticLoggingHelper.ConfigLogger = configLogger;
        }

        public dynamic CreateConfigurationSetting(Type configType)
        {
            try
            {
                StaticLoggingHelper.Debug($"{configType} - Trying to resolve value.");

                // Resolve the key and prefix names
                var keyName = _keyNameResolver.Resolve(configType, _configurationOptions);

                // Pick configuration value reader
                var reader = _configurationReaderResolver.Resolve(configType, _configurationOptions);

                // Pick parser
                var parser = _parserResolver.Resolve(configType, _configurationOptions);

                // Pick constructor
                var valueConstructor = _valueConstructorResolver.Resolve(configType, _configurationOptions);

                // Ensure all required instances are present
                keyName.EnsureNotNull(nameof(IKeyName));
                reader.EnsureNotNull(nameof(IConfigurationReader));
                parser.EnsureNotNull(nameof(IParser));
                valueConstructor.EnsureNotNull(nameof(IValueConstructor));

                // Read and parse the value
                var rawValue = reader.Read(_configuration, configType, keyName, _configurationOptions.MissingConfigurationStratergy);
                var value = parser.Parse(configType, rawValue);

                // Set the value
                var configSetting = valueConstructor?.ConstructValue(configType, value);

                StaticLoggingHelper.Debug($"{configType} - Value resolved successfully.");

                return configSetting;
            }
            catch (Exception e)
            {
                StaticLoggingHelper.Error($"An error occured while attempting to resolve value for {configType}", e);
                throw;
            }
        }


    }
}
