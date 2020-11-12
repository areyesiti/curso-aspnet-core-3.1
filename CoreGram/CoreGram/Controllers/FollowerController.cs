using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreGram.Data.Dtos;
using CoreGram.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreGram.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowerController : ControllerBase
    {
        private readonly FollowerRepository _repository;

        public FollowerController(FollowerRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<FollowerInfoDto>>> GetFollowers(int userId)
        {
            return Ok(_repository.GetFollowers(userId));
        }

        [HttpGet("Following/{userId}")]
        public async Task<ActionResult<IEnumerable<FollowerInfoDto>>> GetFollowing(int userId)
        {
            return Ok(_repository.GetFollowings(userId));
        }

        [HttpPost]
        public async Task<ActionResult<FollowerDto>> Create([FromBody] FollowerDto dto)
        {
            return Ok(await _repository.Create(dto));
        }

        [HttpDelete("{userId}/{followerId}")]
        public async Task<ActionResult<FollowerDto>> Delete(int userId, int followerId)
        {
            return Ok(await _repository.Delete(userId, followerId));
        }
    }
}
