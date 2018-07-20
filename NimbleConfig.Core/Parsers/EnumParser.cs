using System;

namespace NimbleConfig.Core.Parsers
{
    internal class EnumParser: IParser
    {
        object IParser.Parse(Type toType, object value)
        {
            return Enum.Parse(toType, value.ToString());
        }
    }
}
