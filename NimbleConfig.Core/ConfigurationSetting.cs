namespace NimbleConfig.Core
{
    public abstract class ConfigurationSetting<T>
    {
        public virtual T Value { get; set; }
    }
    
}
