using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class IKDbController : ControllerBase
    {
        IUserService _userService;

        private readonly ILogger<IKDbController> _logger;
       
        public IKDbController(IUserService userService,ILogger<IKDbController> logger)
        {
            _userService = userService;
            _logger = logger;
        }


        
        [HttpGet]
        //[Produces("application/json")]
        public IActionResult Get(string sicilNo)
        {
            _logger.LogInformation("Ik sisteminden data çekildi",sicilNo);

            var result = _userService.GetAll(sicilNo);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
    }
}
