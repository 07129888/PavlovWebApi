using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PavlovWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using PavlovWebApi.Storage;

namespace PavlovWebApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private IStorage<CustomerData> _memCache;
        public CustomerController(IStorage<CustomerData> memCache)
        {
            _memCache = memCache;

        }

        [HttpGet]
        public ActionResult<IEnumerable<CustomerData>> Get()
        {
            return Ok(_memCache.All);
        }

        [HttpGet("{id}")]
        public ActionResult<CustomerData> Get(Guid id)
        {
            if (!_memCache.Has(id)) return NotFound("Неизвестный клиент");

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

        public IActionResult Put(Guid id, [FromBody] CustomerData value)
        {
            if (!_memCache.Has(id)) return NotFound("Неизвестный клиент");
            var validationResult = value.Validate();
            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);
            var previousValue = _memCache[id];
            _memCache[id] = value;
            return Ok($"{previousValue.ToString()} обновлено до {value.ToString()}");

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            if (!_memCache.Has(id)) return NotFound("Неизвестный клиент");
            var valueToRemove = _memCache[id];
            _memCache.RemoveAt(id);
            return Ok($"{valueToRemove.ToString()} было удалено");
        }
    }
}
