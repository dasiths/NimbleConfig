using System;
using NimbleConfig.Core.Options;

namespace NimbleConfig.Core.Resolvers
{
    public interface IResolver<out T>
    {
        T Resolve(Type configType, ConfigurationOptions configurationOptions);
    }
}
