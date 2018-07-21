using System;
using System.Linq;
using NimbleConfig.Core.Attributes;
using NimbleConfig.Core.Options;

namespace NimbleConfig.Core.Resolvers
{
    internal static class KeyNameResolver
    {
        public static string GetKeyName(Type type, ConfigurationOptions configurationOptions)
        {
            var prefix = string.Empty;

            // Try to get prefix
            if (configurationOptions.KeyPrefixResolver != null)
            {
                prefix = configurationOptions.KeyPrefixResolver();
            }

            // Try to get resolve custom name
            if (configurationOptions.KeyResolver != null)
            {
                var customKeyName = configurationOptions.KeyResolver(type, prefix);

                if (!string.IsNullOrWhiteSpace(customKeyName))
                {
                    return customKeyName;
                }
            }

            // Try to resolve via SettingInfo attribute
            var attribute = type.CustomAttributes.SingleOrDefault(c => c.AttributeType == typeof(SettingInfo));

            if (attribute?.NamedArguments != null)
            {
                var keyName = attribute.NamedArguments.SingleOrDefault(a => a.MemberName == nameof(SettingInfo.Key));
                return $"{prefix}{keyName.TypedValue.Value}";
            }

            // Revert to type name
            return $"{prefix}{type.Name}";
        }
    }
}