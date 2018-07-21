using System;
using NimbleConfig.Core.ConfigurationReaders;
using NimbleConfig.Core.Parsers;

namespace NimbleConfig.Core.Options
{
    public class ConfigurationOptions
    {
        /// <summary>
        /// Returns the key prefix for a given Type. Returns string.empty if can not resolve.
        /// Signature: [string]Prefix KeyPrefixResolver([Type] settingType)
        /// </summary>
        public Func<Type, string> KeyPrefixResolver { get; set; }

        /// <summary>
        /// Returns the key for a given Type. Returns string.empty if can not resolve.
        /// Signature: [string]Key KeyResolver([Type] settingType, [string]resolvedKeyPrefix)
        /// </summary>
        public Func<Type, string, string> KeyResolver { get; set; }

        /// <summary>
        /// Returns the IParser for a given Type. Returns null if can not resolve.
        /// Signature: [IParseer]Parser ParserResolver([Type] settingType)
        /// </summary>
        public Func<Type, IParser> ParserResolver { get; set; }

        /// <summary>
        /// Returns the IConfigurationReader for a given Type. Returns null if can not resolve.
        /// Signature: [IConfigurationReader]Reader ReaderResolver([Type] settingType)
        /// </summary>
        public Func<Type, IConfigurationReader> ReaderResolver { get; set; }
    }
}
