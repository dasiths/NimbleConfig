using System;
using System.Collections.Generic;
using System.Text;

namespace NimbleConfig.Core.Parsers
{
    internal interface IParser
    {
        object Parse(Type toType, object value);
    }
}
