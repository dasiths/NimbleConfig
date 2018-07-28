using System;
using NimbleConfig.Core.Configuration;
using NimbleConfig.Core.ConfigurationReaders;
using NimbleConfig.Core.Parsers;
using NimbleConfig.Core.ValueConstructors;

namespace NimbleConfig.Core.Options
{
    public static class ConfigurationOptionDelegates
    {
        /// <summary>
        /// Function that resolves a 'IKeyName' for a given type.
        /// </summary>
        /// <param name="configurationSettingType">The type of configuration setting.</param>
        /// <param name="autoResolvedKeyName">The IKeyName resolved by the library.</param>
        /// <returns></returns>
        public delegate IKeyName CustomKeyNameResolverDelegate(Type configurationSettingType, IKeyName autoResolvedKeyName);

        /// <summary>
        /// Function that resolves a 'IParser' for a given type.
        /// </summary>
        /// <param name="configurationSettingType">The type of configuration setting.</param>
        /// <param name="autoResolvedParser">The IParser resolved by the library.</param>
        /// <returns></returns>
        public delegate IParser CustomParserResolverDelegate(Type configurationSettingType, IParser autoResolvedParser);

        /// <summary>
        /// Function that resolves a 'IConfigurationReader' for a given type.
        /// </summary>
        /// <param name="configurationSettingType">The type of configuration setting.</param>
        /// <param name="autoResolvedConfigurationReader">The IConfigurationReader resolved by the library.</param>
        /// <returns></returns>
        public delegate IConfigurationReader CustomConfigurationReaderDelegate(Type configurationSettingType, IConfigurationReader autoResolvedConfigurationReader);

        /// <summary>
        /// Function that resolves a 'IValueConstructor' for a given type.
        /// </summary>
        /// <param name="configurationSettingType">The type of configuration setting.</param>
        /// <param name="autoResolvedValueConstructor">The IValueConstructor resolved by the library.</param>
        /// <returns></returns>
        public delegate IValueConstructor CustomValueConstructorDelegate(Type configurationSettingType, IValueConstructor autoResolvedValueConstructor);
    }
}
