using System;

namespace NimbleConfig.Core.Parsers
{
    public interface IParser
    {
        object Parse(Type configType, object value);
    }
}
