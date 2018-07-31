using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Configuration;
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
    public static class DependencyInjectionExtensions
    {
        public static ServiceProvider SetupConfigurationDependencies(this IConfiguration configuration, 
            Assembly[] assemblies,
            IConfigurationOptions configurationOptions = null)
        {
            // Configure your DI Container
            // We are using 'Microsoft.Extensions.DependencyInjection' in this example
            // but you can use your favourite one like Autofac, StructureMap, Ninject etc

            var services = new ServiceCollection();

            // Add single IConfiguration
            services.AddSingleton(configuration);

            // Add configuration options instance
            configurationOptions = configurationOptions ?? ConfigurationOptions.Create();
            services.AddSingleton<IConfigurationOptions>((s) => configurationOptions);

            // Add required resolvers
            services.AddSingleton<IResolver<IKeyName>, KeyNameResolver>();
            services.AddSingleton<IResolver<IParser>, ParserResolver>();
            services.AddSingleton<IResolver<IConfigurationReader>, ConfigurationReaderResolver>();
            services.AddSingleton<IResolver<IValueConstructor>, ValueConstructorResolver>();

            // Add configuration factory
            services.AddSingleton<IConfigurationFactory, ConfigurationFactory>();

            var settingTypes = GetConfigurationSettings(assemblies);

            foreach (var settingType in settingTypes)
            {
                services.AddSingleton(settingType, (s) =>
                        {
                            var factory = s.GetService<IConfigurationFactory>();
                            return factory.CreateConfigurationSetting(settingType);
                        });
            }

            return services.BuildServiceProvider();
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