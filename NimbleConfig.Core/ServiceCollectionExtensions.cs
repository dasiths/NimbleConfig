using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace NimbleConfig.Core
{
    public static class ServiceCollectionExtensions
    {
        public static void AddConfigurationSettingsFrom(this IServiceCollection services, Assembly[] assemblies)
        {
            var settingTypes = GetConfigurationSettings(assemblies);

            foreach (var settingType in settingTypes)
            {
                services.AddSingleton(settingType, (s) =>
                        {
                            var factory = s.GetService<ValueFactory>();
                            return factory.CreateConfigurationSetting(settingType);
                        });
            }
        }

        public static IEnumerable<Type> GetConfigurationSettings(Assembly[] assemblies)
        {
            var serviceType = typeof(ConfigurationSetting<>);

            return assemblies.SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.BaseType != null &&
                               type.BaseType.IsGenericType &&
                               type.BaseType.GetGenericTypeDefinition() == serviceType &&
                               !type.GetTypeInfo().IsAbstract);
        }
    }
}