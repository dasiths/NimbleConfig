using System;
using Microsoft.Extensions.Configuration;

namespace NimbleConfig.Core.Extensions
{
    internal static class ConfigurationExtensions
    {
        internal static object ReadComplexType(this IConfiguration configuration, Type complexType, string key)
        {
            return configuration.GetSection(key).Get(complexType);
        }

        internal static object ReadValueType(this IConfiguration configuration, string key)
        {
            return configuration[key];
        }
    }
}
