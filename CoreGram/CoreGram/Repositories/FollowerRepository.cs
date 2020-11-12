using AutoMapper;
using CoreGram.Data;
using CoreGram.Data.Dtos;
using CoreGram.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreGram.Repositories
{
    public class FollowerRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public FollowerRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
      
        public IEnumerable<FollowerInfoDto> GetFollowers(int userId)
        {            
            var user = _context.Users.Find(userId);
            if (user == null)
            {
                throw new Exception("El usuario no existe");
            }
            var model = _context.Follower
                .Where(x => x.UserId == userId)
                .Include(x => x.UserFollower)
                    .ThenInclude(x => x.Profile)
                .OrderBy(x => x.UserFollower.Login)
                .ToList();

            return _mapper.Map<List<Follower>, List<FollowerInfoDto>>(model);
        }

        public IEnumerable<FollowerInfoDto> GetFollowings(int userId)
        {
            var user = _context.Users.Find(userId);
            if (user == null)
            {
                throw new Exception("El usuario no existe");
            }
            var model = _context.Follower
                .Where(x => x.FollowerId == userId)
                .Include(x => x.UserFollowing)
                    .ThenInclude(x => x.Profile)
                .OrderBy(x => x.UserFollower.Login)
                .ToList();

            return _mapper.Map<List<Follower>, List<FollowerInfoDto>>(model);
        }

        public async Task<FollowerDto> Create(FollowerDto dto)
        {
            if (dto.UserId == dto.FollowerId)
            {
                throw new Exception("No puedes seguirte a ti mismo");
            }

            var follower = await _context.Follower.FindAsync(dto.UserId, dto.FollowerId);            

            if (follower != null)
            {
                throw new Exception("Ya sigues a este usuario");
            }

            var model = _mapper.Map<Follower>(dto);
            _context.Follower.Add(model);
            await _context.SaveChangesAsync();
            return _mapper.Map<FollowerDto>(model);

        }
        public async Task<FollowerDto> Delete(int userId, int followerId)
        {
            var model = await _context.Follower.FindAsync(userId, followerId);
            if (model == null)
            {
                throw new Exception("No se ha encontrado el usuario");
            }
            _context.Follower.Remove(model);
            await _context.SaveChangesAsync();
            return _mapper.Map<FollowerDto>(model);
        }
    }
}
