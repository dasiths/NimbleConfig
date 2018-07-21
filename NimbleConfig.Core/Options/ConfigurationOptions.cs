using System;
using NimbleConfig.Core.Parsers;

namespace NimbleConfig.Core.Options
{
    public class ConfigurationOptions
    {
        public Func<string> KeyPrefixResolver { get; set; }
        public Func<Type, string, string> KeyResolver { get; set; }
        public Func<Type, IParser> ParserResolver { get; set; }
    }
}
