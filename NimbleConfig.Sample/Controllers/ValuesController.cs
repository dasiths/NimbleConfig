using System.Collections.Generic;
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

        public ValuesController(SomeSetting someSetting, SomeNumber someNumber, SomeComplexType someComplexType)
        {
            _someSetting = someSetting;
            _someNumber = someNumber;
            _someComplexType = someComplexType;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2", _someSetting.Value, _someNumber.Value.ToString(), _someComplexType.SomeProp };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
