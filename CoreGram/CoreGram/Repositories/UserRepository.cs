using AutoMapper;
using CoreGram.Data;
using CoreGram.Data.Dtos;
using CoreGram.Data.Models;
using CoreGram.Helpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreGram.Repositories
{
    public class UserRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UserRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDto>> GetAll()
        {

            var model = await _context.Users.Include(x => x.Profile).ToListAsync();
            return _mapper.Map<List<User>, List<UserDto>>(model);
        }

        public async Task<UserInfoDto> GetById(int id)
        {
            var model = await _context.Users
                .Include(x => x.Profile)
                .Where(x => x.Id == id).FirstOrDefaultAsync();

            if (model == null)
            {
                throw new NotFoundException("No se ha encontrado el usuario con id: " + id);
            }

            return _mapper.Map<UserInfoDto>(model);            
        }

        public async Task<UserDto> Create(UserDto dto)
        {
            var model = _mapper.Map<User>(dto);
            _context.Users.Add(model);
            await _context.SaveChangesAsync();

            var result = await _context.Users.FindAsync(model.Id);

            if (result == null)
            {
                throw new NotFoundException("No se ha encontrado el usuario con id: " + model.Id);
            }

            return _mapper.Map<UserDto>(result);                    
        }

        public async Task<UserDto> Update(int id, UserDto dto)
        {
            var model = _mapper.Map<User>(dto);

            if (id != model.Id)
            {
                throw new UnprocessableEntityException("El id no coincide");
            }

            _context.Entry(model).State = EntityState.Modified;
            var result = await _context.SaveChangesAsync();

            return _mapper.Map<UserDto>(result);
        }

        public async Task<UserDto> Delete(int id)
        {
            var model = await _context.Users.FindAsync(id);
            if (model == null)
            {
                throw new NotFoundException("No se ha encontrado el usuario");
            }

            _context.Users.Remove(model);
            await _context.SaveChangesAsync();

            return _mapper.Map<UserDto>(model);
        }
    }
}
