using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreGram.Data.Dtos;
using CoreGram.Data.Models;
using CoreGram.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreGram.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly UserProfileRepository _repository;

        public UserProfileController(UserProfileRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserProfileDto>> GetById(int id)
        {
            return Ok(await _repository.GetById(id));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UserProfileDto>> Update(int id, [FromBody]UserProfileDto dto)
        {
            return Ok(await _repository.Update(id, dto));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<UserProfileDto>> Delete(int id)
        {
            return Ok(await _repository.Delete(id));
        }
    }
}
