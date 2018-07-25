using System;
using NimbleConfig.Core.Configuration;
using NimbleConfig.Core.Extensions;
using NimbleConfig.Core.Options;
using NimbleConfig.Core.ValueConstructors;

namespace NimbleConfig.Core.Resolvers
{
    public class ValueConstructorResolver : IResolver<IValueConstructor>
    {
        private static readonly IValueConstructor ComplexTypeValueConstructor = new ComplexTypeValueConstructor();
        private static readonly IValueConstructor GenericTypeValueConstructor = new GenericTypeValueConstructor();

        public IValueConstructor Resolve(Type configType, ConfigurationOptions configurationOptions)
        {
            var autoResolvedConstructor = ResolveInternally(configType, configurationOptions);

            // Try to resolve a custom constructor from options
            var customConstructor = configurationOptions.CustomValueConstructor?.Invoke(configType, autoResolvedConstructor);

            return customConstructor ?? autoResolvedConstructor;
        }

        public IValueConstructor ResolveInternally(Type configType, ConfigurationOptions configurationOptions)
        {
            var settingType = configType.GetConfigurationSettingType();

            switch (settingType)
            {
                case ConfigurationSettingType.ComplexType:
                    return ComplexTypeValueConstructor;
                case ConfigurationSettingType.GenericType:
                    return GenericTypeValueConstructor;
                case ConfigurationSettingType.None:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return null;
        }
    }
}
