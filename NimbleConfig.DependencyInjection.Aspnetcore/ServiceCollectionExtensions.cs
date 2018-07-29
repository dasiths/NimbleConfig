using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using NimbleConfig.Core.Factory;
using NimbleConfig.Core.Configuration;
using NimbleConfig.Core.ConfigurationReaders;
using NimbleConfig.Core.Extensions;
using NimbleConfig.Core.Options;
using NimbleConfig.Core.Parsers;
using NimbleConfig.Core.Resolvers;
using NimbleConfig.Core.ValueConstructors;

namespace NimbleConfig.DependencyInjection.Aspnetcore
{
    public static class ServiceCollectionExtensions
    {
        public static void AddConfigurationSettings(this IServiceCollection services,
            IConfigurationOptions configurationOptions = null,
            ServiceLifetime settingLifetime = ServiceLifetime.Singleton,
            Type[] ignoreTypes = null)
        {
            services.AddConfigurationSettingsFrom(Assembly.GetEntryAssembly(), configurationOptions, settingLifetime, ignoreTypes);
        }

        public static void AddConfigurationSettingsFrom(this IServiceCollection services,
            Assembly assembly,
            IConfigurationOptions configurationOptions = null,
            ServiceLifetime settingLifetime = ServiceLifetime.Singleton,
            Type[] ignoreTypes = null)
        {
            services.AddConfigurationSettingsFrom(new[] { assembly }, configurationOptions, settingLifetime, ignoreTypes);
        }

        public static void AddConfigurationSettingsFrom(this IServiceCollection services,
            Assembly[] assemblies,
            IConfigurationOptions configurationOptions = null,
            ServiceLifetime settingLifetime = ServiceLifetime.Singleton,
            Type[] ignoreTypes = null)
        {
            // Add required default resolvers as singletons
            services.AddSingleton<IResolver<IKeyName>, KeyNameResolver>();
            services.AddSingleton<IResolver<IParser>, ParserResolver>();
            services.AddSingleton<IResolver<IConfigurationReader>, ConfigurationReaderResolver>();
            services.AddSingleton<IResolver<IValueConstructor>, ValueConstructorResolver>();

            // Add configuration options instance
            configurationOptions = configurationOptions ?? ConfigurationOptionFactory.Create();
            services.AddSingleton<IConfigurationOptions>((s) => configurationOptions);

            // Construct the configuration factory service descriptor
            var configDescriptor = new ServiceDescriptor(typeof(ConfigurationFactory),
                typeof(ConfigurationFactory),
                settingLifetime);

            services.Add(configDescriptor);

            // Get the setting types in the assemblies specified
            var settingTypes = GetConfigurationSettings(assemblies);

            // If an ignore list is specified
            if (ignoreTypes != null)
            {
                settingTypes = settingTypes.Except(ignoreTypes);
            }

            foreach (var settingType in settingTypes)
            {
                // Construct our service descriptor
                var settingDescriptor = new ServiceDescriptor(settingType,
                        (s) =>
                            {
                                var factory = s.GetService<ConfigurationFactory>();
                                return factory.CreateConfigurationSetting(settingType);
                            },
                        settingLifetime);

                services.Add(settingDescriptor);
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