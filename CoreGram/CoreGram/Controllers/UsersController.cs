using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoreGram.Data;
using CoreGram.Data.Models;
using CoreGram.Repositories;
using CoreGram.Data.Dtos;

namespace CoreGram.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {        
        private readonly UserRepository _repository;

        public UsersController(UserRepository repository)
        {            
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            return Ok(await _repository.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserInfoDto>> GetUser(int id)
        {
            return Ok(await _repository.GetById(id));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UserDto>> PutUser(int id, [FromBody]UserDto dto)
        {
            return Ok(await _repository.Update(id, dto));
        }
        
        [HttpPost]        
        public async Task<ActionResult<UserDto>> PostUser([FromBody]UserDto dto)
        {
            return Ok(await _repository.Create(dto));
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserDto>> DeleteUser(int id)
        {
            return Ok(await _repository.Delete(id));
        }
    }
}
