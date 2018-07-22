using System;
using System.Linq;
using NimbleConfig.Core.Attributes;
using NimbleConfig.Core.Configuration;
using NimbleConfig.Core.Options;

namespace NimbleConfig.Core.Resolvers
{
    public class KeyNameResolver: IResolver<IKeyName>
    {
        public IKeyName Resolve(Type type, ConfigurationOptions configurationOptions)
        {
            var prefix = string.Empty;

            // Try to get prefix
            if (configurationOptions.KeyPrefixResolver != null)
            {
                prefix = configurationOptions.KeyPrefixResolver(type);
            }

            // Try to get resolve custom name
            if (configurationOptions.KeyResolver != null)
            {
                var customKeyName = configurationOptions.KeyResolver(type, prefix);

                if (!string.IsNullOrWhiteSpace(customKeyName))
                {
                    return new KeyName(string.Empty, customKeyName);
                }
            }

            // Try to resolve via SettingInfo attribute
            var attribute = type.CustomAttributes.SingleOrDefault(c => c.AttributeType == typeof(SettingInfo));

            if (attribute?.NamedArguments != null)
            {
                var keyName = attribute.NamedArguments.SingleOrDefault(a => a.MemberName == nameof(SettingInfo.Key));
                var value = keyName.TypedValue.Value;
                return new KeyName(prefix, value.ToString());
            }

            // Revert to type name
            return new KeyName(prefix, type.Name);
        }
    }
}