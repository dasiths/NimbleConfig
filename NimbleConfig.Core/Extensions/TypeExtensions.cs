using System;

namespace NimbleConfig.Core.Extensions
{
    internal static class TypeExtensions
    {
        internal static Type GetGenericTypeOfConfigurationSetting(this Type configType)
        {
            // Todo: better inspection and guards
            return configType.BaseType.GetGenericArguments()[0];
        }
    }
}
