using System;
using NimbleConfig.Core.Configuration;
using NimbleConfig.Core.ConfigurationReaders;
using NimbleConfig.Core.Extensions;
using NimbleConfig.Core.Options;

namespace NimbleConfig.Core.Resolvers
{
    public static class ConfigurationReaderResolver
    {
        private static readonly IConfigurationReader GenericValueTypeConfigurationReader =
            new GenericValueTypeConfigurationReader();

        private static readonly IConfigurationReader GenericNonValueTypeConfigurationReader = 
            new GenericNonValueTypeConfigurationReader();

        private static readonly IConfigurationReader ComplexTypeConfigurationReader =
            new ComplexTypeConfigurationReader();

        public static IConfigurationReader ResolveReader(Type type,
            ConfigurationOptions configurationOptions)
        {
            var resolver = configurationOptions.ReaderResolver;

            // Try to resolve a custom reader defined in options
            var reader = resolver?.Invoke(type);

            if (reader != null)
            {
                return reader;
            }

            if (type.GetConfigurationSettingType() == ConfigurationSettingType.GenericType)
            {
                var genericType = type.GetGenericTypeOfConfigurationSetting();

                return genericType.IsValueType
                    ? GenericValueTypeConfigurationReader
                    : GenericNonValueTypeConfigurationReader;
            }
            else
            {
                return ComplexTypeConfigurationReader;
            }
        }
    }
}
