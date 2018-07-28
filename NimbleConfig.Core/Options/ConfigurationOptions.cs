namespace NimbleConfig.Core.Options
{
    public class ConfigurationOptions : IConfigurationOptions
    {
        public ConfigurationOptionDelegates.CustomKeyNameResolverDelegate CustomKeyName { get; set; }

        public ConfigurationOptionDelegates.CustomParserResolverDelegate CustomParser { get; set; }

        public ConfigurationOptionDelegates.CustomConfigurationReaderDelegate CustomConfigurationReader { get; set; }

        public ConfigurationOptionDelegates.CustomValueConstructorDelegate CustomValueConstructor { get; set; }

        /// <summary>
        /// The stratergy to use when a configuration setting is missing
        /// </summary>
        public MissingConfigurationStratergy MissingConfigurationStratergy { get; set; }
    }
}
