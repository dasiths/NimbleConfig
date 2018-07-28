using System;
using Microsoft.Extensions.Configuration;
using NimbleConfig.Core.Configuration;
using NimbleConfig.Core.ConfigurationReaders;
using NimbleConfig.Core.Parsers;

namespace NimbleConfig.Core.ValueConstructors
{
    public interface IValueConstructor
    {
        dynamic ConstructValue(Type configType, dynamic value);
    }
}
