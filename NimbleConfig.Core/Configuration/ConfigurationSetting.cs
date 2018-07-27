using System;
using NimbleConfig.Core.Logging;

namespace NimbleConfig.Core.Configuration
{
    public abstract class ConfigurationSetting<TValue>
    {
        public virtual TValue Value { get; set; }

        /// <summary>
        /// This method gets called when setting the value, use this to customize the logic
        /// </summary>
        /// <param name="value">The value read from the configuration</param>
        public virtual void SetValue(TValue value)
        {
            if (value == null || value.Equals(default(TValue)))
            {
                StaticLoggingHelper.Info($"{GetType().Name} value has been resolved as null/default");
                Value = default(TValue);
            }

            Value = value;
        }
    }

}
