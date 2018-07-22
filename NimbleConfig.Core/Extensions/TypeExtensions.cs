using System;
using NimbleConfig.Core.Configuration;

namespace NimbleConfig.Core.Extensions
{
    public static class TypeExtensions
    {
        public static ConfigurationSettingType GetConfigurationSettingType(this Type configType)
        {
            if (IsComplexTypeConfigurationSetting(configType))
            {
                return ConfigurationSettingType.ComplexType;
            }

            if (IsGenericTypeConfigurationSetting(configType))
            {
                return ConfigurationSettingType.GenericType;
            }

            return ConfigurationSettingType.None;
        }

        public static bool IsComplexTypeConfigurationSetting(this Type configType)
        {
            return typeof(IComplexConfigurationSetting).IsAssignableFrom(configType);
        }

        public static bool IsGenericTypeConfigurationSetting(this Type configType)
        {
            try
            {
                var baseType = GetImmidiateTypeAfterObject(configType);
                return baseType.GetGenericTypeDefinition() == typeof(ConfigurationSetting<>);
            }
            catch (Exception)
            {
                return false;
            }
        }

        internal static Type GetGenericTypeOfConfigurationSetting(this Type configType)
        {
            // Todo: better inspection and guards

            if (IsGenericTypeConfigurationSetting(configType))
            {
                var baseType = GetImmidiateTypeAfterObject(configType);
                return baseType.GetGenericArguments()[0];
            }

            throw new ArgumentException($"The given type {configType} is not a type of {typeof(ConfigurationSetting<>)}.", nameof(configType));
        }

        private static Type GetImmidiateTypeAfterObject(this Type type)
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
