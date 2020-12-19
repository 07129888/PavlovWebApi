using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PavlovWebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace PavlovWebApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private static List<CustomerData> _memCache = new List<CustomerData>();

        [HttpGet]
        public ActionResult<IEnumerable<CustomerData>> Get()
        {
            return Ok(_memCache);
        }

        [HttpGet("{id}")]
        public ActionResult<CustomerData> Get(int id)
        {
            if (_memCache.Count <= id) return NotFound("Неизвестный клиент");

            return Ok(_memCache[id]);
        }

        [HttpPost]
        public IActionResult Post([FromBody] CustomerData value)
        {
            var validationResult = value.Validate();
            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);
            _memCache.Add(value);
            return Ok($"{value.ToString()} добавлено");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] CustomerData value)
        {
            if (_memCache.Count <= id) return NotFound("Неизвестный клиент");
            var validationResult = value.Validate();
            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);
            var previousValue = _memCache[id];
            _memCache[id] = value;
            return Ok($"{previousValue.ToString()} обновлено до {value.ToString()}");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (_memCache.Count <= id) return NotFound("Неизвестный клиент");
            var valueToRemove = _memCache[id];
            _memCache.RemoveAt(id);
            return Ok($"{valueToRemove.ToString()} было удалено");
        }
    }
}
