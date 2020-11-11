using CoreGram.Data;
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
        public UserProfileRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<UserProfile> GetById(int profileId)
        {
            var model = await _context.UserProfile.FindAsync(profileId);
            if (model == null)
            {
                throw new Exception("Perfil de usuario no encontrado");
            }
            return model;
        }

        public async Task<UserProfile> Update(int userId, UserProfile dto)
        {
            if (dto.Id == 0) dto.Id = userId;

            var user = await _context.Users.FindAsync(dto.Id);

            if (user == null)
            {
                throw new Exception("El usuario no existe");
            }

            var profile = await _context.UserProfile.FindAsync(userId);

            if (profile == null)
            {                
                _context.UserProfile.Add(dto);
            }
            else
            {
                _context.Entry(profile).State = EntityState.Detached;                
                _context.UserProfile.Update(dto);
            }

            await _context.SaveChangesAsync();
            return dto;

        }

        public async Task<UserProfile> Delete(int profileId)
        {
            var model = await _context.UserProfile.FindAsync(profileId);
            if (model == null)
            {
                throw new Exception("Perfil de usuario no encontrado");
            }
            _context.UserProfile.Remove(model);
            _context.SaveChangesAsync();
            return model;
        }
    }
}
