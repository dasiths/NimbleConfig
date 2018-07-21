using System;
using NimbleConfig.Core.Configuration;

namespace NimbleConfig.Core.Extensions
{
    internal static class TypeExtensions
    {
        internal static Type GetGenericTypeOfConfigurationSetting(this Type configType)
        {
            // Todo: better inspection and guards
            var baseType = GetImmidiateTypeAfterObject(configType);

            if (baseType.GetGenericTypeDefinition() == typeof(ConfigurationSetting<>))
            {
                return baseType.GetGenericArguments()[0];
            }

            throw new ArgumentException($"The given type {configType} is not a type of {typeof(ConfigurationSetting<>)}.", nameof(configType));
        }

        private static Type GetImmidiateTypeAfterObject(Type type)
        {
            if (type.IsClass && type.BaseType != null)
            {
                if (type.BaseType == typeof(object))
                {
                    return type;
                }

                return GetImmidiateTypeAfterObject(type.BaseType);
            }

            throw new ArgumentException($"The given type {type} is not a class.", nameof(type));

        }
    }
}
