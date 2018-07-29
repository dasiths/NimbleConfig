namespace NimbleConfig.Core.Options
{
    public interface IConfigurationOptions
    {
        ConfigurationOptionDelegates.CustomReaderFunc CustomReader { get; set; }
        ConfigurationOptionDelegates.CustomKeyNameFunc CustomKeyName { get; set; }
        ConfigurationOptionDelegates.CustomParserFunc CustomParser { get; set; }
        ConfigurationOptionDelegates.CustomConstructorFunc CustomConstructor { get; set; }
        MissingConfigurationStratergy MissingConfigurationStratergy { get; set; }
    }
}