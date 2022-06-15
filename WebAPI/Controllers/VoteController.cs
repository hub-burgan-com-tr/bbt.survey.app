using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoteController : ControllerBase
    {
        IVoteService _voteService;

        public VoteController(IVoteService voteService)
        {
            _voteService = voteService;
        }

        [HttpPost]
        public IActionResult Post(Vote vote)
        {
            
            var result = _voteService.Add(vote);
            if (result.Success)
            {
                return Ok(result);

            }
            return BadRequest(result.Message);
            
        }

       
    }
}
