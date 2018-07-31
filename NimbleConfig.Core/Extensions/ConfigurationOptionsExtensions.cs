using System;
using System.Linq;
using NimbleConfig.Core.Configuration;
using NimbleConfig.Core.Options;

namespace NimbleConfig.Core.Extensions
{
    public static class ConfigurationOptionsExtensions
    {
        public static IConfigurationOptions WithNamingScheme(this IConfigurationOptions configurationOptions,
            ConfigurationOptionDelegates.CustomKeyNameFunc resolverFunction)
        {
            ConfigurationOptionDelegates.CustomKeyNameFunc currentResolverFunc = null;

            currentResolverFunc = configurationOptions.CustomKeyName;
            if (currentResolverFunc == null)
            {
                configurationOptions.CustomKeyName = resolverFunction;
            }
            else
            {
                // Chain the functions
                configurationOptions.CustomKeyName = (type, name) =>
                {
                    var customName = currentResolverFunc.Invoke(type, name);
                    return resolverFunction.Invoke(type, customName);
                };
            }

            return configurationOptions;
        }

        public static IConfigurationOptions WithReader(this IConfigurationOptions configurationOptions,
            ConfigurationOptionDelegates.CustomReaderFunc resolverFunction)
        {
            ConfigurationOptionDelegates.CustomReaderFunc currentResolverFunc = null;

            currentResolverFunc = configurationOptions.CustomReader;
            if (currentResolverFunc == null)
            {
                configurationOptions.CustomReader = resolverFunction;
            }
            else
            {
                // Chain the functions
                configurationOptions.CustomReader = (type, reader) =>
                {
                    var customReader = currentResolverFunc.Invoke(type, reader);
                    return resolverFunction.Invoke(type, customReader);
                };
            }

            return configurationOptions;
        }

        public static IConfigurationOptions WithParser(this IConfigurationOptions configurationOptions,
            ConfigurationOptionDelegates.CustomParserFunc resolverFunction)
        {
            ConfigurationOptionDelegates.CustomParserFunc currentResolverFunc = null;

            currentResolverFunc = configurationOptions.CustomParser;
            if (currentResolverFunc == null)
            {
                configurationOptions.CustomParser = resolverFunction;
            }
            else
            {
                // Chain the functions
                configurationOptions.CustomParser = (type, parser) =>
                {
                    var customParser = currentResolverFunc.Invoke(type, parser);
                    return resolverFunction.Invoke(type, customParser);
                };
            }

            return configurationOptions;
        }

        public static IConfigurationOptions WithConstructor(this IConfigurationOptions configurationOptions,
            ConfigurationOptionDelegates.CustomConstructorFunc resolverFunction)
        {
            ConfigurationOptionDelegates.CustomConstructorFunc currentResolverFunc = null;

            currentResolverFunc = configurationOptions.CustomConstructor;
            if (currentResolverFunc == null)
            {
                configurationOptions.CustomConstructor = resolverFunction;
            }
            else
            {
                // Chain the functions
                configurationOptions.CustomConstructor = (type, constructor) =>
                {
                    var customConstructor = currentResolverFunc.Invoke(type, constructor);
                    return resolverFunction.Invoke(type, customConstructor);
                };
            }

            return configurationOptions;
        }

        public static IConfigurationOptions WithGlobalPrefix(this IConfigurationOptions configurationOptions,
            string prefix,
            Type[] onlyForTheseTypes = null,
            Type[] exceptTheseTypes = null)
        {
            ConfigurationOptionDelegates.CustomKeyNameFunc keyNameResolverFunc = (type, name) =>
            {
                if (onlyForTheseTypes != null && !onlyForTheseTypes.Contains(type))
                {
                    return name;
                }

                if (exceptTheseTypes != null && exceptTheseTypes.Contains(type))
                {
                    return name;
                }

                return new KeyName(prefix, name.QualifiedKeyName);
            };

            return configurationOptions.WithNamingScheme(keyNameResolverFunc);
        }

        public static IConfigurationOptions WithExceptionForMissingSettings(this IConfigurationOptions configurationOptions)
        {
            configurationOptions.MissingConfigurationStratergy = MissingConfigurationStratergy.ThrowException;
            return configurationOptions;
        }

        public static IConfigurationOptions WithIgnoringMissingSettings(this IConfigurationOptions configurationOptions)
        {
            configurationOptions.MissingConfigurationStratergy = MissingConfigurationStratergy.IgnoreAndUseDefaultOrNull;
            return configurationOptions;
        }
    }
}
