using System;
using NimbleConfig.Core.Parsers;

namespace NimbleConfig.Core.ValueConstructors
{
    public class ComplexTypeValueConstructor : IValueConstructor
    {
        public dynamic ConstructValue(Type configType, object value, IParser parser)
        {
            value = parser == null ? value : parser.Parse(configType, value);
            return value;
        }
    }
}