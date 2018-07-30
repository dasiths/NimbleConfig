using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using NimbleConfig.Core.Options;

namespace NimbleConfig.DependencyInjection.Aspnetcore
{
    public static class ServiceCollectionExtensions
    {
        public static void SetupConfigurationSettings(this IServiceCollection services,
            IConfigurationOptions configurationOptions = null,
            ServiceLifetime settingLifetime = ServiceLifetime.Singleton,
            Type[] ignoreTypes = null)
        {
            services.SetupConfigurationSettingsFrom(Assembly.GetEntryAssembly(), configurationOptions, settingLifetime, ignoreTypes);
        }

        public static void SetupConfigurationSettingsFrom(this IServiceCollection services,
            Assembly assembly,
            IConfigurationOptions configurationOptions = null,
            ServiceLifetime settingLifetime = ServiceLifetime.Singleton,
            Type[] ignoreTypes = null)
        {
            services.SetupConfigurationSettingsFrom(new[] { assembly }, configurationOptions, settingLifetime, ignoreTypes);
        }

        public static void SetupConfigurationSettingsFrom(this IServiceCollection services,
            Assembly[] assemblies,
            IConfigurationOptions configurationOptions = null,
            ServiceLifetime settingLifetime = ServiceLifetime.Singleton,
            Type[] ignoreTypes = null)
        {
            var builder = ConfigInjectionBuilder.Create(services);
            builder.Assemblies = assemblies;
            builder.Options = configurationOptions;
            builder.SettingLifetime = settingLifetime;
            builder.IgnoreTypes = ignoreTypes;
            builder.AndBuild();
        }

        public static ConfigInjectionBuilder AddConfigurationSettings(this IServiceCollection services)
        {
            return ConfigInjectionBuilder.Create(services);
        }
    }
}