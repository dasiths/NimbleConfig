using System;
using Microsoft.Extensions.Configuration;
using NimbleConfig.Core.Configuration;
using NimbleConfig.Core.ConfigurationReaders;
using NimbleConfig.Core.Extensions;
using NimbleConfig.Core.Parsers;

namespace NimbleConfig.Core.ValueConstructors
{
    public class ComplexTypeValueConstructor : IValueConstructor
    {
        public dynamic ConstructValue(Type configType, dynamic value)
        {
            return value;
        }
    }
}