using System;
using Microsoft.Extensions.Configuration;

namespace NimbleConfig.Core.ConfigurationReaders
{
    public interface IConfigurationReader
    {
        object Read(IConfiguration configuration, Type configType, string key);
    }
}
