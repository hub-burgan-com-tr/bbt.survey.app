using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedLockNet;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoteController : ControllerBase
    {
        private readonly IVoteService _voteService;

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

