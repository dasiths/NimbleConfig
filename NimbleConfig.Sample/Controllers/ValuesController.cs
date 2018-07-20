using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace NimbleConfig.Sample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly SomeSetting _someSetting;
        private readonly SomeNumber _someNumber;
        private readonly SomeComplexType _someComplexType;
        private readonly VeryStrangeSetting _veryStrangeSetting;
        private readonly ArraySetting _arraySetting;

        public ValuesController(SomeSetting someSetting,
            SomeNumber someNumber,
            SomeComplexType someComplexType,
            VeryStrangeSetting veryStrangeSetting,
            ArraySetting arraySetting)
        {
            _someSetting = someSetting;
            _someNumber = someNumber;
            _someComplexType = someComplexType;
            _veryStrangeSetting = veryStrangeSetting;
            _arraySetting = arraySetting;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1",
                "value2",
                _someSetting.Value,
                _someNumber.Value.ToString(),
                _someComplexType.SomeProp,
                _veryStrangeSetting.Value.ToString(),
                _arraySetting.Value.First().ToString()
            };
        }
    }
}
