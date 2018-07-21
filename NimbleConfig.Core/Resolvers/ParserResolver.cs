using System;
using NimbleConfig.Core.Options;
using NimbleConfig.Core.Parsers;

namespace NimbleConfig.Core.Resolvers
{
    internal static class ParserResolver
    {
        private static readonly GenericParser GenericParser = new GenericParser();
        private static readonly EnumParser EnumParser = new EnumParser();

        internal static IParser GetParser(Type valueType, ConfigurationOptions configurationOptions)
        {
            // Todo: Guard for value types
            // Todo: Inspect internals when choosing best parser

            // Try to resolve a custom parser defined in options
            if (configurationOptions.ParserResolver != null)
            {
                var parser = configurationOptions.ParserResolver(valueType);
                return parser;
            }

            if (valueType.IsEnum)
            {
                return EnumParser;
            }

            return GenericParser;
        }
    }
}
