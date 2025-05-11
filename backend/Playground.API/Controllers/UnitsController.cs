using Microsoft.AspNetCore.Mvc;
using Playground.Application.Providers;

namespace Playground.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UnitsController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var units = UnitProvider.GetAll();
            return Ok(units);
        }
    }
}
