using System;
using System.Linq;
using NimbleConfig.Core.Attributes;
using NimbleConfig.Core.Configuration;
using NimbleConfig.Core.Options;

namespace NimbleConfig.Core.Resolvers
{
    public class KeyNameResolver: IResolver<IKeyName>
    {
        public IKeyName Resolve(Type type, IConfigurationOptions configurationOptions)
        {
            var autoResolvedKeyName = ResolveInternally(type, configurationOptions);
            
            // Try to resolve a custom key name from options
            var customKeyName = configurationOptions.CustomKeyName?.Invoke(type, autoResolvedKeyName);

            return customKeyName ?? autoResolvedKeyName;
        }

        private IKeyName ResolveInternally(Type type, IConfigurationOptions configurationOptions)
        {
            var prefix = string.Empty;

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