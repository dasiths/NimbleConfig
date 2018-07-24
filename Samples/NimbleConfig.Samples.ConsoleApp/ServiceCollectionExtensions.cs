using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using NimbleConfig.Core.Configuration;
using NimbleConfig.Core.ConfigurationReaders;
using NimbleConfig.Core.Extensions;
using NimbleConfig.Core.Factory;
using NimbleConfig.Core.Options;
using NimbleConfig.Core.Parsers;
using NimbleConfig.Core.Resolvers;
using NimbleConfig.Core.ValueConstructors;

namespace NimbleConfig.Samples.ConsoleApp
{
    public static class ServiceCollectionExtensions
    {
        public static void AddConfigurationSettingsFrom(this IServiceCollection services, Assembly[] assemblies, ConfigurationOptions configurationOptions = null)
        {
            // Add configuration options instance
            configurationOptions = configurationOptions ?? new ConfigurationOptions();
            services.AddSingleton((s) => configurationOptions);

            // Add required resolvers
            services.AddSingleton<IResolver<IKeyName>,KeyNameResolver>();
            services.AddSingleton<IResolver<IParser>, ParserResolver>();
            services.AddSingleton<IResolver<IConfigurationReader>, ConfigurationReaderResolver>();
            services.AddSingleton<IResolver<IValueConstructor>, ValueConstructorResolver>();

            // Add configuration factory
            services.AddSingleton<ConfigurationFactory>();

            var settingTypes = GetConfigurationSettings(assemblies);

            foreach (var settingType in settingTypes)
            {
                services.AddSingleton(settingType, (s) =>
                        {
                            var factory = s.GetService<ConfigurationFactory>();
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