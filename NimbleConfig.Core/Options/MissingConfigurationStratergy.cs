namespace NimbleConfig.Core.Options
{
    public enum MissingConfigurationStratergy
    {
        /// <summary>
        /// Ignores the missing key and tries to set null or default(T) as value
        /// </summary>
        IgnoreAndUseDefaultOrNull = 0,
        /// <summary>
        /// Throw an exception when a key is missing from the configuration
        /// </summary>
        ThrowException = 1
    }
}