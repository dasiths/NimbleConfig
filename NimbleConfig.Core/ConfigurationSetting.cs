namespace NimbleConfig.Core
{
    public abstract class ConfigurationSetting<TValue>
    {
        public virtual TValue Value { get; protected set; }

        public virtual void SetValue(object configValue)
        {
            Value = (TValue)System.Convert.ChangeType(configValue, typeof(TValue));
        }
    }
    
}
