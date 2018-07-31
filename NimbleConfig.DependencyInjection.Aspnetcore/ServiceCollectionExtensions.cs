using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using NimbleConfig.Core.Logging;
using NimbleConfig.Core.Options;

namespace NimbleConfig.DependencyInjection.Aspnetcore
{
    public static class ServiceCollectionExtensions
    {
        public static ConfigInjectionBuilder AddConfigurationSettings(this IServiceCollection services)
        {
            return ConfigInjectionBuilder.Create(services);
        }
    }
}