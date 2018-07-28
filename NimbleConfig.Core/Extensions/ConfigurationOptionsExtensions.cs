using System;
using System.Linq;
using NimbleConfig.Core.Configuration;
using NimbleConfig.Core.Options;

namespace NimbleConfig.Core.Extensions
{
    public static class ConfigurationOptionsExtensions
    {
        public static void SetGlobalPrefix(this IConfigurationOptions configurationOptions, string prefix, Type[] onlyForTheseTypes = null, Type[] exceptTheseTypes = null)
        {
            configurationOptions.CustomKeyName = (type, name) =>
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
        }
    }
}
