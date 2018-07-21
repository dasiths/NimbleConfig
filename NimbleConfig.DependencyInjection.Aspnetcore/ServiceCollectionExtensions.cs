using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using NimbleConfig.Core.Factory;
using NimbleConfig.Core.Configuration;
using NimbleConfig.Core.Options;

namespace NimbleConfig.DependencyInjection.Aspnetcore
{
    public static class ServiceCollectionExtensions
    {
        public static void AddConfigurationSettingsFrom(this IServiceCollection services, Assembly[] assemblies, ConfigurationOptions configurationOptions = null)
        {
            configurationOptions = configurationOptions ?? new ConfigurationOptions();

            services.AddSingleton((s) => configurationOptions);
            services.AddSingleton<ValueFactory>();
            
            var settingTypes = GetConfigurationSettings(assemblies);
            var complexSettingTypes = GetComplexConfigurationSettings(assemblies);

            foreach (var settingType in settingTypes)
            {
                services.AddSingleton(settingType, (s) =>
                        {
                            var factory = s.GetService<ValueFactory>();
                            return factory.CreateConfigurationSetting(settingType);

                        });
            }

            foreach (var settingType in complexSettingTypes)
            {
                services.AddSingleton(settingType, (s) =>
                {
                    var factory = s.GetService<ValueFactory>();
                    return factory.CreateComplexConfigurationSetting(settingType);

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

        public static IEnumerable<Type> GetComplexConfigurationSettings(Assembly[] assemblies)
        {
            var serviceType = typeof(IComplexConfigurationSetting);
            return assemblies.SelectMany(assembly => assembly.GetTypes())
                .Where(type => serviceType.IsAssignableFrom(type));
        }
    }
}