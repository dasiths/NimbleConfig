using System;
using System.Collections.Generic;
using System.Text;

namespace NimbleConfig.Core.Parsers
{
    public interface IParser
    {
        object Parse(Type toType, object value);
    }
}
