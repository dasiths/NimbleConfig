using System;
using NimbleConfig.Core.Configuration;
using NimbleConfig.Core.Extensions;
using NimbleConfig.Core.Options;
using NimbleConfig.Core.Parsers;

namespace NimbleConfig.Core.Resolvers
{
    public class ParserResolver: IResolver<IParser>
    {
        private static readonly DefaultParser DefaultParser = new DefaultParser();

        public IParser Resolve(Type configType, ConfigurationOptions configurationOptions)
        {
            // Get the configuration value type
            var valueType = GetConfigurationValueType(configType);
            var autoResolvedParser = ResolveInternally(valueType, configurationOptions);

            // Try to resolve a custom parser from options, use configType as this is what the function expects
            var customParser = configurationOptions.CustomParser?.Invoke(configType, autoResolvedParser);

            return customParser ?? autoResolvedParser;
        }

        public IParser ResolveInternally(Type valueType, ConfigurationOptions configurationOptions)
        {
            return DefaultParser;
        }

        private static Type GetConfigurationValueType(Type configType)
        {
            var valueType = typeof(object);
            var settingType = configType.GetConfigurationSettingType();

            switch (settingType)
            {
                case ConfigurationSettingType.ComplexType:
                    valueType = configType;
                    break;
                case ConfigurationSettingType.GenericType:
                    valueType = configType.GetGenericTypeOfConfigurationSetting();
                    break;
                case ConfigurationSettingType.None:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return valueType;
        }
    }
}
