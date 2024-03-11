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
        private readonly IDistributedLockFactory _distributedLockFactory;

        public VoteController(IVoteService voteService, IDistributedLockFactory distributedLockFactory)
        {
            _voteService = voteService;
            _distributedLockFactory = distributedLockFactory;
        }

        [HttpPost]
        public IActionResult Post(Vote vote)
        {
            string lockKey = $"VoteLock:{vote.UserId}";
            using (var redisLock = _distributedLockFactory.CreateLock(lockKey, TimeSpan.FromSeconds(10)))
            {
                if (redisLock.IsAcquired)
                {
                    var result = _voteService.Add(vote);
                    if (result.Success)
                    {
                        return Ok(result);
                    }
                    return BadRequest(result.Message);
                }
                else
                {
                    return BadRequest("Cannot acquire lock. Please try again later.");
                }
            }

        }


    }
}
