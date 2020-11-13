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
    public class PostController : ControllerBase
    {
        public readonly PostRepository _repository;
        public PostController(PostRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostDto>>> GetAll()
        {
            return Ok(await _repository.GetAll());
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<PostDto>>> GetByUser(int userId)
        {
            return Ok(await _repository.GetByUser(userId));
        }

        [HttpGet("Followings/{userId}")]
        public async Task<ActionResult<IEnumerable<PostDto>>> GetByFollowings(int userId)
        {
            return Ok(await _repository.GetByFollowings(userId));
        }

        [HttpPost]
        public async Task<ActionResult<PostDto>> Create([FromBody] PostDto dto)
        {
            return Ok(await _repository.Create(dto));
        }

        [HttpDelete("{postId}")]
        public async Task<ActionResult<PostDto>> Delete(int postId)
        {
            return Ok(await _repository.Delete(postId));
        }

    }
}
