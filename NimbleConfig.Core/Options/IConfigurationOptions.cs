namespace NimbleConfig.Core.Options
{
    public interface IConfigurationOptions
    {
        ConfigurationOptionDelegates.CustomConfigurationReaderDelegate CustomConfigurationReader { get; set; }
        ConfigurationOptionDelegates.CustomKeyNameResolverDelegate CustomKeyName { get; set; }
        ConfigurationOptionDelegates.CustomParserResolverDelegate CustomParser { get; set; }
        ConfigurationOptionDelegates.CustomValueConstructorDelegate CustomValueConstructor { get; set; }
        MissingConfigurationStratergy MissingConfigurationStratergy { get; set; }
    }
}