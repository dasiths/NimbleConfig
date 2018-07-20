using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using NimbleConfig.Core.Attributes;
using NimbleConfig.Core.Extensions;

namespace NimbleConfig.Core.Factory
{
    public class ValueFactory
    {
        private readonly IConfiguration _configuration;
        public ValueFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public dynamic CreateConfigurationSetting(Type configType)
        {
            // Todo: handle missing config settings

            dynamic config = Activator.CreateInstance(configType);

            var key = GetKeyName(configType);
            object value = null;

            var genericType = configType.GetGenericTypeOfConfigurationSetting();

            value = genericType.IsValueType ? _configuration.ReadValueType(key) : _configuration.ReadComplexType(genericType, key);

            config.SetValue(value);
            return config;
        }

        public dynamic CreateComplexConfigurationSetting(Type configType)
        {
            var key = GetKeyName(configType);
            var value = _configuration.ReadComplexType(configType, key);
            return value;
        }

        private string GetKeyName(Type type)
        {
            var attribute = type.CustomAttributes.SingleOrDefault(c => c.AttributeType == typeof(SettingInfo));

            if (attribute?.NamedArguments != null)
            {
                var keyName = attribute.NamedArguments.SingleOrDefault(a => a.MemberName == nameof(SettingInfo.Key));
                return keyName.TypedValue.Value.ToString();
            }

            return type.Name;
        }
    }
}
