using System;
using Microsoft.Extensions.Configuration;
using NimbleConfig.Core.Configuration;

namespace NimbleConfig.Core.ConfigurationReaders
{
    public interface IConfigurationReader
    {
        object Read(IConfiguration configuration, Type configType, IKeyName keyName);
    }
}
