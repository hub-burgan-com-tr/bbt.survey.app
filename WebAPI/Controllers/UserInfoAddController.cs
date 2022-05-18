using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserInfoAddController : ControllerBase
    {
        IUserInfoService _userInfoService;
        private readonly ILogger<IKDbController> _logger;
        public UserInfoAddController(IUserInfoService userInfoService, ILogger<IKDbController> logger)
        {
            _userInfoService = userInfoService;
            _logger = logger;

        }


        [HttpPost]
        public IActionResult Post(UserInfo userInfo)
        {
            _logger.LogInformation("Kullanıcı bilgisi post edildi",userInfo);
            //var userName = HttpContext.User.Identity.Name;
            
            var result = _userInfoService.Add(userInfo);
            
            if (result.Success)
            {
                return Ok(result);
               

            }
            return BadRequest(result.Message);
        }




        [HttpGet]

        public IActionResult Get(string sicilNo)
        {
            
            var result=_userInfoService.GetAll(sicilNo);
            _logger.LogInformation("Kullanıcı bilgisi datası Get edildi");
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }
    }
}
