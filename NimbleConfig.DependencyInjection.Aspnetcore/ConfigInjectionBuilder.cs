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

namespace NimbleConfig.DependencyInjection.Aspnetcore
{
    public class ConfigInjectionBuilder
    {
        public readonly IServiceCollection Services;
        public IConfigurationOptions Options { get; set; }
        public ServiceLifetime SettingLifetime { get; set; }
        public Type[] IgnoreTypes { get; set; }
        public Assembly[] Assemblies { get; set; }

        private bool _isBuilt = false;

        public ConfigInjectionBuilder(IServiceCollection services)
        {
            Services = services ?? throw new ArgumentNullException(nameof(services));
            SettingLifetime = ServiceLifetime.Singleton;
            Options = ConfigurationOptions.Create();
            Assemblies = new[] { Assembly.GetEntryAssembly() };
        }

        public static ConfigInjectionBuilder Create(IServiceCollection services)
        {
            return new ConfigInjectionBuilder(services);
        }

        public ConfigInjectionBuilder WithConfigurationOptions(IConfigurationOptions configurationOptions)
        {
            Options = configurationOptions;
            return this;
        }

        public ConfigInjectionBuilder ScanningAssemblies(Assembly[] assemblies)
        {
            Assemblies = assemblies;
            return this;
        }

        public ConfigInjectionBuilder WithSingletonInstances()
        {
            SettingLifetime = ServiceLifetime.Singleton;
            return this;
        }

        public ConfigInjectionBuilder WithScopedInstances()
        {
            SettingLifetime = ServiceLifetime.Scoped;
            return this;
        }

        public ConfigInjectionBuilder WithInstancesOf(ServiceLifetime serviceLifetime)
        {
            SettingLifetime = serviceLifetime;
            return this;
        }

        public ConfigInjectionBuilder IgnoringTheseTypes(Type[] types)
        {
            IgnoreTypes = types;
            return this;
        }

        public void AndBuild()
        {
            if (_isBuilt)
            {
                return;
            }

            _isBuilt = true;

            // Add required default resolvers as singletons
            Services.AddSingleton<IResolver<IKeyName>, KeyNameResolver>();
            Services.AddSingleton<IResolver<IParser>, ParserResolver>();
            Services.AddSingleton<IResolver<IConfigurationReader>, ConfigurationReaderResolver>();
            Services.AddSingleton<IResolver<IValueConstructor>, ValueConstructorResolver>();

            // Add configuration options instance
            Options = Options ?? Core.Options.ConfigurationOptions.Create();
            Services.AddSingleton<IConfigurationOptions>((s) => Options);

            // Construct the configuration factory service descriptor
            var configDescriptor = new ServiceDescriptor(typeof(ConfigurationFactory),
                typeof(ConfigurationFactory),
                SettingLifetime);

            Services.Add(configDescriptor);

            // Get the setting types in the assemblies specified
            var settingTypes = SettingScanner.GetConfigurationSettings(Assemblies);

            // If an ignore list is specified
            if (IgnoreTypes != null)
            {
                settingTypes = settingTypes.Except(IgnoreTypes);
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
                    SettingLifetime);

                Services.Add(settingDescriptor);
            }
        }
    }
}