using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;


namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UserTestController : ControllerBase
    {
        private readonly ILogger<UserTestController> _logger;
        IUserTestService _userTestService;


        public UserTestController(IUserTestService userTestService, ILogger<UserTestController> logger)
        {
            _userTestService = userTestService;
            _logger = logger;

        }




        [HttpPost]
        public IActionResult Post(UserTest userTest)
        {
            _logger.LogInformation("Vote bilgisi kullanýcý bilgileriyle post yapýldý", userTest);

            //var userName = HttpContext.User.Identity.Name;
            //userTest.Department = userName;
            var result = _userTestService.Add(userTest);
            if (result.Success)
            {
                return Ok(result);

            }
            return BadRequest(result.Message);
        }


        [HttpPut]

        public IActionResult Put(UserTest userTest)
        {
            _logger.LogInformation("Put kullanýlmayan");
            var result = _userTestService.Update(userTest);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }


        [HttpGet]
        public IActionResult Get()
        {
            

            var result = _userTestService.GetAll();
            _logger.LogInformation("Vote bilgisi Get edildi");
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
            
        }
    }
}
