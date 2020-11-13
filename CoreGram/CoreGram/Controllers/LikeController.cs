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
    public class LikeController : ControllerBase
    {
        private readonly LikeRepository _repository;

        public LikeController(LikeRepository repository)
        {
            _repository = repository;
        }
        
        [HttpGet("{postId}")]
        public async Task<ActionResult<int>> GetByPost(int postId)
        {
            return Ok(await _repository.GetByPost(postId));
        }

        [HttpPost]
        public async Task<ActionResult<LikeDto>> Like([FromBody] LikeDto dto)
        {
            return Ok(await _repository.Like(dto));
        }
    }
}
