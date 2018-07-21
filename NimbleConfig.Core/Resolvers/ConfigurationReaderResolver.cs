using System;
using NimbleConfig.Core.Configuration;
using NimbleConfig.Core.ConfigurationReaders;
using NimbleConfig.Core.Extensions;
using NimbleConfig.Core.Options;

namespace NimbleConfig.Core.Resolvers
{
    public static class ConfigurationReaderResolver
    {
        private static readonly ValueTypeConfigurationReader ValueTypeConfigurationReader =
            new ValueTypeConfigurationReader();

        private static readonly ComplexTypeConfigurationReader ComplexTypeConfigurationReader =
            new ComplexTypeConfigurationReader();

        public static IConfigurationReader ResolveReader(Type type,
            ConfigurationOptions configurationOptions)
        {
            if (type.GetConfigurationSettingType() == ConfigurationSettingType.ValueType)
            {
                return ValueTypeConfigurationReader;
            } else
            {
                return ComplexTypeConfigurationReader;
            }
        }
    }
}
