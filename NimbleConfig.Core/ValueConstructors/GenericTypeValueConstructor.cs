using System;
using Microsoft.Extensions.Configuration;
using NimbleConfig.Core.Configuration;
using NimbleConfig.Core.ConfigurationReaders;
using NimbleConfig.Core.Extensions;
using NimbleConfig.Core.Parsers;

namespace NimbleConfig.Core.ValueConstructors
{
    public class GenericTypeValueConstructor : IValueConstructor
    {
        public dynamic ConstructValue(IConfiguration configuration,
            Type configType,
            IKeyName keyName,
            IConfigurationReader reader,
            IParser parser)
        {
            configuration.EnsureNotNull(nameof(configuration));
            keyName.EnsureNotNull(nameof(keyName));
            reader.EnsureNotNull(nameof(reader));
            parser.EnsureNotNull(nameof(parser));

            dynamic config = Activator.CreateInstance(configType);
            var rawValue = reader.Read(configuration, configType, keyName);
            var genericType = configType.GetGenericTypeOfConfigurationSetting();
            dynamic value =  parser.Parse(genericType, rawValue);

            config.SetValue(value);
            return config;
        }
    }
}