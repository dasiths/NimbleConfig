using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using NimbleConfig.Core.Configuration;
using NimbleConfig.Core.ConfigurationReaders;
using NimbleConfig.Core.Extensions;
using NimbleConfig.Core.Factory;
using NimbleConfig.Core.Logging;
using NimbleConfig.Core.Options;
using NimbleConfig.Core.Parsers;
using NimbleConfig.Core.Resolvers;
using NimbleConfig.Core.ValueConstructors;

namespace NimbleConfig.DependencyInjection.Aspnetcore
{
    public class ConfigInjectionBuilder
    {
        private readonly IServiceCollection _services;
        private IConfigurationOptions Options { get; set; }
        private ServiceLifetime SettingLifetime { get; set; }
        private Type[] IgnoreTypes { get; set; }
        private Assembly[] Assemblies { get; set; }
        private Type LoggerType { get; set; }

        private bool _isBuilt = false;

        public ConfigInjectionBuilder(IServiceCollection services)
        {
            _services = services ?? throw new ArgumentNullException(nameof(services));
            SettingLifetime = ServiceLifetime.Singleton;
            Options = ConfigurationOptions.Create();
            Assemblies = new[] { Assembly.GetEntryAssembly() };
            LoggerType = null;
        }

        public static ConfigInjectionBuilder Create(IServiceCollection services)
        {
            return new ConfigInjectionBuilder(services);
        }

        public ConfigInjectionBuilder UsingOptionsIn(IConfigurationOptions configurationOptions)
        {
            Options = configurationOptions;
            return this;
        }

        public ConfigInjectionBuilder ByScanning(Assembly[] assemblies)
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

        public ConfigInjectionBuilder WithLifetemOf(ServiceLifetime serviceLifetime)
        {
            SettingLifetime = serviceLifetime;
            return this;
        }

        public ConfigInjectionBuilder IgnoringTheseTypes(Type[] types)
        {
            IgnoreTypes = types;
            return this;
        }

        public ConfigInjectionBuilder UsingLogger<T>() where T : IConfigLogger
        {
            LoggerType = typeof(T);
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
            _services.AddSingleton<IResolver<IKeyName>, KeyNameResolver>();
            _services.AddSingleton<IResolver<IParser>, ParserResolver>();
            _services.AddSingleton<IResolver<IConfigurationReader>, ConfigurationReaderResolver>();
            _services.AddSingleton<IResolver<IValueConstructor>, ValueConstructorResolver>();

            // Add configuration options instance
            Options = Options ?? ConfigurationOptions.Create();
            _services.AddSingleton<IConfigurationOptions>((s) => Options);

            // Construct the configuration factory service descriptor
            var configDescriptor = new ServiceDescriptor(typeof(IConfigurationFactory),
                typeof(ConfigurationFactory),
                SettingLifetime);

            _services.Add(configDescriptor);

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
                        var factory = s.GetService<IConfigurationFactory>();
                        return factory.CreateConfigurationSetting(settingType);
                    },
                    SettingLifetime);

                _services.Add(settingDescriptor);
            }

            if (LoggerType != null)
            {
                _services.AddSingleton(typeof(IConfigLogger), LoggerType);
            }
        }
    }
}