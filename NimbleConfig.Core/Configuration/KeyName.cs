namespace NimbleConfig.Core.Configuration
{
    public class KeyName : IKeyName
    {
        public string Prefix { get; set; }
        public string Name { get; set; }

        public KeyName(string prefix, string name)
        {
            Prefix = prefix;
            Name = name;
        }

        public string QualifiedKeyName => $"{Prefix}{Name}";
    }
}
