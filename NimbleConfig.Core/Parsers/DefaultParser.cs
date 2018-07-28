using System;
using NimbleConfig.Core.Configuration;
using NimbleConfig.Core.Extensions;

namespace NimbleConfig.Core.Parsers
{
    public class DefaultParser : IParser
    {
        object IParser.Parse(Type configType, object value)
        {
            if (value == null)
            {
                return null;
            }

            if (configType.GetConfigurationSettingType() == ConfigurationSettingType.GenericType)
            {
                var genericType = configType.GetGenericTypeOfConfigurationSetting();

                var elementType = genericType.GetElementType();

                // Handle complex type arrays like ConfigurationSetting<int[]>
                if (genericType.IsArray && elementType != null && !elementType.IsValueType)
                {
                    return ParseArray(value, elementType);
                }

                // Handle value type
                return ChangeType(value, genericType);
            }

            // Handle complex type
            return ChangeType(value, configType);
        }

        private static object ParseArray(object value, Type elementType)
        {
            var values = (object[]) value;
            var destinationArray = Array.CreateInstance(elementType, values.Length);

            for (var i = 0; i < values.Length; i++)
            {
                var convertedValue = ChangeType(values[i], elementType);
                destinationArray.SetValue(convertedValue, i);
            }

            return destinationArray;
        }

        private static object ChangeType(object value, Type type)
        {
            return Convert.ChangeType(value, type);
        }
    }
}
