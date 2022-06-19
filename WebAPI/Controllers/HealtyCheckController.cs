using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealtyCheckController : ControllerBase
    {
        [HttpGet]

        public IActionResult Get()
        {
            return Ok(new
            {
                message = true
            });
        }
    }
}
