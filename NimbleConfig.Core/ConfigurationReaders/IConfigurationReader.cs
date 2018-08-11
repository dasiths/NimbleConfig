using System;
using Microsoft.Extensions.Configuration;
using NimbleConfig.Core.Configuration;
using NimbleConfig.Core.Options;

namespace NimbleConfig.Core.ConfigurationReaders
{
    public interface IConfigurationReader
    {
        object Read(IConfiguration configuration,
            Type configType,
            IKeyName keyName);

        bool ValueExists(IConfiguration configuration,
            Type configType,
            IKeyName keyName);
    }
}
