using System;

namespace NimbleConfig.Core.Parsers
{
    public class DefaultParser : IParser
    {
        object IParser.Parse(Type toType, object value)
        {
            if (value == null)
            {
                return null;
            }

            var elementType = toType.GetElementType();

            // Handle complex type arrays
            if (toType.IsArray && elementType != null && !elementType.IsValueType)
            {
                return ParseArray(value, elementType);
            }

            return ChangeType(value, toType);
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
