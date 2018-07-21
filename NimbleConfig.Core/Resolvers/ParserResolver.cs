using System;
using NimbleConfig.Core.Configuration;
using NimbleConfig.Core.Extensions;
using NimbleConfig.Core.Options;
using NimbleConfig.Core.Parsers;

namespace NimbleConfig.Core.Resolvers
{
    internal static class ParserResolver
    {
        private static readonly GenericParser GenericParser = new GenericParser();
        private static readonly EnumParser EnumParser = new EnumParser();

        internal static IParser ResolveParser(Type configType, ConfigurationOptions configurationOptions)
        {
            // Todo: Add guards

            // Get the configuration value type
            Type valueType = typeof(object);
            var settingType = configType.GetConfigurationSettingType();

            switch (settingType)
            {
                case ConfigurationSettingType.ComplexType:
                    valueType = configType;
                    break;
                case ConfigurationSettingType.ValueType:
                    valueType = configType.GetGenericTypeOfConfigurationSetting();
                    break;
                case ConfigurationSettingType.None:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            // Try to resolve a custom parser defined in options
            var customParser = configurationOptions.ParserResolver?.Invoke(valueType);

            if (customParser != null)
            {
                return customParser;
            }

            if (valueType.IsEnum)
            {
                return EnumParser;
            }

            return GenericParser;
        }
    }
}
