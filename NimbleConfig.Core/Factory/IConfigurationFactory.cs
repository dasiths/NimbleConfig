using System;

namespace NimbleConfig.Core.Factory
{
    public interface IConfigurationFactory
    {
        dynamic CreateConfigurationSetting(Type configType);
    }
}