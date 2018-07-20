using System;
using System.Collections.Generic;
using System.Text;

namespace NimbleConfig.Core.Parsers
{
    internal static class ParserPicker
    {
        private static readonly GenericParser GenericParser = new GenericParser();
        private static readonly EnumParser EnumParser = new EnumParser();

        internal static IParser GetParser(Type valueType)
        {
            // Todo: Guard for value types
            // Todo: Inspect internals when choosing best parser

            if (valueType.IsEnum)
            {
                return EnumParser;
            }

            return GenericParser;
        }
    }
}
