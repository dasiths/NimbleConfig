namespace NimbleConfig.Core.Options
{
    public class ConfigurationOptions : IConfigurationOptions
    {
        public ConfigurationOptionDelegates.CustomKeyNameFunc CustomKeyName { get; set; }

        public ConfigurationOptionDelegates.CustomParserFunc CustomParser { get; set; }

        public ConfigurationOptionDelegates.CustomReaderFunc CustomReader { get; set; }

        public ConfigurationOptionDelegates.CustomConstructorFunc CustomConstructor { get; set; }

        /// <summary>
        /// The stratergy to use when a configuration setting is missing
        /// </summary>
        public MissingConfigurationStratergy MissingConfigurationStratergy { get; set; }

        public static IConfigurationOptions Create()
        {
            return new ConfigurationOptions();
        }
    }
}
