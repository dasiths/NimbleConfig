using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NimbleConfig.Core.Configuration;
using NimbleConfig.Core.Extensions;

namespace NimbleConfig.DependencyInjection.Aspnetcore
{
    public class SettingScanner
    {
        public static IEnumerable<Type> GetConfigurationSettings(Assembly[] assemblies)
        {
            return assemblies.SelectMany(assembly => assembly.GetTypes())
                .Where(type =>
                {
                    var settingType = type.GetConfigurationSettingType();
                    return settingType != ConfigurationSettingType.None;
                });
        }
    }
}