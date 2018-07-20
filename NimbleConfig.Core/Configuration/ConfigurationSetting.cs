using System;
using NimbleConfig.Core.Parsers;

namespace NimbleConfig.Core.Configuration
{
    public abstract class ConfigurationSetting<TValue>
    {
        public virtual TValue Value { get; protected set; }

        public virtual void SetValue(object configValue)
        {
            var parser = ParserPicker.GetParser(typeof(TValue));
            Value = (TValue)parser.Parse(typeof(TValue), configValue);
        }
    }

}
