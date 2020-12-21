using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PavlovWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetRandomController
    {
        [HttpGet]
        public string Get()
        {
            return $"Генерация числа между 0 и 100. Вам выпало: {new Random().Next(0,100)}";
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return $"Генерация числа между 0 и {id}. Вам выпало: {new Random().Next(0, id)}";
        }
    }
}
