using System;
using NimbleConfig.Core.Options;
using NimbleConfig.Core.Parsers;

namespace NimbleConfig.Core.Resolvers
{
    public class ParserResolver: IResolver<IParser>
    {
        private static readonly DefaultParser DefaultParser = new DefaultParser();

        public IParser Resolve(Type configType, ConfigurationOptions configurationOptions)
        {
            var autoResolvedParser = ResolveInternally(configType, configurationOptions);

            // Try to resolve a custom parser from options
            var customParser = configurationOptions.CustomParser?.Invoke(configType, autoResolvedParser);

            return customParser ?? autoResolvedParser;
        }

        public IParser ResolveInternally(Type configType, ConfigurationOptions configurationOptions)
        {
            return DefaultParser;
        }
    }
}
