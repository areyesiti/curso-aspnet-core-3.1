using AutoMapper;
using CoreGram.Data;
using CoreGram.Data.Dtos;
using CoreGram.Data.Models;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreGram.Repositories
{
    public class UserProfileRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public UserProfileRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserProfileDto> GetById(int profileId)
        {
            var model = await _context.UserProfile.FindAsync(profileId);
            if (model == null)
            {
                throw new Exception("Perfil de usuario no encontrado");
            }
            return _mapper.Map<UserProfileDto>(model);
        }

        public async Task<UserProfileDto> Update(int userId, UserProfileDto dto)
        {
            var dtoId = _mapper.Map<UserProfile>(dto);

            if (dtoId.Id == 0)
            {
                //dtoId.UserId = userId;
                dtoId.Id = userId;
            }

            var model = await _context.Users.FindAsync(dtoId.Id);

            if (model == null)
            {
                throw new Exception("El usuario no existe");
            }

            var profile = await _context.UserProfile.FindAsync(userId);

            if (profile == null)
            {                
                _context.UserProfile.Add(dtoId);
            }
            else
            {
                _context.Entry(profile).State = EntityState.Detached;                
                _context.UserProfile.Update(dtoId);
            }

            await _context.SaveChangesAsync();
            return dto;

        }

        public async Task<UserProfileDto> Delete(int profileId)
        {
            var model = await _context.UserProfile.FindAsync(profileId);
            if (model == null)
            {
                throw new Exception("Perfil de usuario no encontrado");
            }
            _context.UserProfile.Remove(model);

            await _context.SaveChangesAsync();
            return _mapper.Map<UserProfileDto>(model);
        }
    }
}
