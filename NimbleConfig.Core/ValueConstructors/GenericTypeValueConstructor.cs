using System;
using NimbleConfig.Core.Extensions;
using NimbleConfig.Core.Parsers;

namespace NimbleConfig.Core.ValueConstructors
{
    public class GenericTypeValueConstructor : IValueConstructor
    {
        public dynamic ConstructValue(Type configType, object value, IParser parser)
        {
            dynamic config = Activator.CreateInstance(configType);
            var genericType = configType.GetGenericTypeOfConfigurationSetting();
            object ParserFunc(object rawValue) => parser.Parse(genericType, rawValue);
            config.SetValue(value, (Func<object, object>)ParserFunc);
            return config;
        }
    }
}