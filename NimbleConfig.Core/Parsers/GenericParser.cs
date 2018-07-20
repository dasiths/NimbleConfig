using System;

namespace NimbleConfig.Core.Parsers
{
    internal class GenericParser : IParser
    {
        object IParser.Parse(Type toType, object value)
        {
            return Convert.ChangeType(value, toType);
        }
    }
}