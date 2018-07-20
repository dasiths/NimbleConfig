using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NimbleConfig.Sample.Settings;

namespace NimbleConfig.Sample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly SomeSetting _someSetting;
        private readonly SomeNumberSetting _someNumberSetting;
        private readonly SomeComplexTypeSetting _someComplexTypeSetting;
        private readonly VeryStrangeSetting _veryStrangeSetting;
        private readonly ArraySetting _arraySetting;
        private readonly EnumSetting _enumSetting;
        private readonly BoolSetting _boolSetting;

        public ValuesController(SomeSetting someSetting,
            SomeNumberSetting someNumberSetting,
            SomeComplexTypeSetting someComplexTypeSetting,
            VeryStrangeSetting veryStrangeSetting,
            ArraySetting arraySetting,
            EnumSetting enumSetting,
            BoolSetting boolSetting)
        {
            _someSetting = someSetting;
            _someNumberSetting = someNumberSetting;
            _someComplexTypeSetting = someComplexTypeSetting;
            _veryStrangeSetting = veryStrangeSetting;
            _arraySetting = arraySetting;
            _enumSetting = enumSetting;
            _boolSetting = boolSetting;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1",
                "value2",
                _someSetting.Value,
                _someNumberSetting.Value.ToString(),
                _someComplexTypeSetting.SomeProp,
                _veryStrangeSetting.Value.ToString(),
                _arraySetting.Value.First().ToString(),
                _enumSetting.Value.ToString(),
                _boolSetting.Value.ToString()
            };
        }
    }
}
