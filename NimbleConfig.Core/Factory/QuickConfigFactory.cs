using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using NimbleConfig.Core.Configuration;
using NimbleConfig.Core.Extensions;
using NimbleConfig.Core.Options;
using NimbleConfig.Core.Resolvers;

namespace NimbleConfig.Core.Factory
{
    public static class QuickConfigFactory
    {
        public static T GetSetting<T>(IConfiguration configuration, IConfigurationOptions options = null)
        {
            return (T)GetSetting(typeof(T), configuration, options);
        }

        public static object GetSetting(Type type, IConfiguration configuration, IConfigurationOptions options = null)
        {
            if (type.GetConfigurationSettingType() == ConfigurationSettingType.None)
            {
                throw new ArgumentException($"{type} is not a Configuration Setting type.", nameof(type));
            }

            var factory = Create(configuration, options);
            return factory.CreateConfigurationSetting(type);
        }

        private static IConfigurationFactory Create(IConfiguration configuration, IConfigurationOptions options = null)
        {
            configuration.EnsureNotNull(nameof(configuration));

            // Configure the core pieces
            var keynameResolver = new KeyNameResolver();
            var parserResolver = new ParserResolver();
            var configResolver = new ConfigurationReaderResolver();
            var constructorResolver = new ValueConstructorResolver();

            options = options ?? ConfigurationOptions.Create();

            var configuratinFactory = new ConfigurationFactory(configuration,
                options,
                keynameResolver,
                configResolver,
                parserResolver,
                constructorResolver);
            return configuratinFactory;
        }
    }
}
