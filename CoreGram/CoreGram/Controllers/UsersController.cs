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
            _repository.Inicializar();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return Ok(await _repository.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            return Ok(await _repository.GetById(id));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            return Ok(await _repository.Update(id, user));
        }
        
        [HttpPost]        
        public async Task<ActionResult<User>> PostUser(User user)
        {
            return Ok(await _repository.Create(user));
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            return Ok(await _repository.Delete(id));
        }
    }
}
