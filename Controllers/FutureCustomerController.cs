using Microsoft.AspNetCore.Mvc;
using PavlovWebApi.Models;
using PavlovWebApi.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PavlovWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FutureCustomerController 
    {
        private IStorage<CustomerData> _memCache;
        public FutureCustomerController(IStorage<CustomerData> memCache)
        {
            _memCache = memCache;
        }

        [HttpGet]
        public string Get()
        {
            string result="";
            foreach (CustomerData x in _memCache.All)
                if (x.VisitDate >= DateTime.Now)
                    result += "\n" + x.ToString(); 
            return result;
        }

    }
}
