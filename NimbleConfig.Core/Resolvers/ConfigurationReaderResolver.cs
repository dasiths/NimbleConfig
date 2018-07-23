using System;
using NimbleConfig.Core.Configuration;
using NimbleConfig.Core.ConfigurationReaders;
using NimbleConfig.Core.Extensions;
using NimbleConfig.Core.Options;

namespace NimbleConfig.Core.Resolvers
{
    public class ConfigurationReaderResolver : IResolver<IConfigurationReader>
    {
        private static readonly IConfigurationReader GenericValueTypeConfigurationReader =
            new GenericValueTypeConfigurationReader();

        private static readonly IConfigurationReader GenericNonValueTypeConfigurationReader =
            new GenericNonValueTypeConfigurationReader();

        private static readonly IConfigurationReader ComplexTypeConfigurationReader =
            new ComplexTypeConfigurationReader();

        public IConfigurationReader Resolve(Type type, ConfigurationOptions configurationOptions)
        {
            var autoResolvedReader = ResolveInternally(type, configurationOptions);

            // Try to resolve a custom reader from options
            var reader = configurationOptions.CustomConfigurationReader?.Invoke(type, autoResolvedReader);

            return reader ?? autoResolvedReader;
        }

        public IConfigurationReader ResolveInternally(Type type, ConfigurationOptions configurationOptions)
        {
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
