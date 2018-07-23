using System;
using Microsoft.Extensions.Configuration;
using NimbleConfig.Core.Configuration;
using NimbleConfig.Core.ConfigurationReaders;
using NimbleConfig.Core.Parsers;

namespace NimbleConfig.Core.ValueConstructors
{
    public interface IValueConstructor
    {
        dynamic ConstructValue(IConfiguration configuration,
            Type configType,
            IKeyName keyName,
            IConfigurationReader reader,
            IParser parser);
    }
}
