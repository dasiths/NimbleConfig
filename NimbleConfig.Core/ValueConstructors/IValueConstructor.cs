using System;
using NimbleConfig.Core.Parsers;

namespace NimbleConfig.Core.ValueConstructors
{
    public interface IValueConstructor
    {
        dynamic ConstructValue(Type configType, object value, IParser parser);
    }
}
