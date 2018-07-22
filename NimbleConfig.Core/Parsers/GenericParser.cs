using System;

namespace NimbleConfig.Core.Parsers
{
    internal class GenericParser : IParser
    {
        object IParser.Parse(Type toType, object value)
        {
            // Handle complex type arrays
            if (toType.IsArray && !toType.GetElementType().IsValueType)
            {
                object[] values = (object[])value;
                Array destinationArray = Array.CreateInstance(toType.GetElementType(), values.Length);
                Array.Copy(values, destinationArray, values.Length);

                return destinationArray;
            }

            return Convert.ChangeType(value, toType);
        }
    }
}