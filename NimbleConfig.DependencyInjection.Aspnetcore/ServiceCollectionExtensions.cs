using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using NimbleConfig.Core.Factory;
using NimbleConfig.Core.Configuration;
using NimbleConfig.Core.Extensions;
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
            return assemblies.SelectMany(assembly => assembly.GetTypes())
                .Where(type =>
                {
                    var settingType = type.GetConfigurationSettingType();
                    return settingType != ConfigurationSettingType.None;
                });
        }
    }
}