using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using PavlovWebApi.Models;
using Serilog;

namespace PavlovWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class VersionController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<string> Get()
        {
            var versionInfo = new Version
            {
                Company = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyCompanyAttribute>().Company,
                Product = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyProductAttribute>().Product,
                ProductVersion = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion
            };
            Log.Information($"Запрос к версии приложения");
            Log.Information($"Актуальная версия {versionInfo.ProductVersion}");
            Log.Debug($"Полная информация о версии: {@versionInfo}");
            return Ok(versionInfo);
        }
    }
}
