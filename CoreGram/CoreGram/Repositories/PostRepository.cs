using AutoMapper;
using CoreGram.Data;
using CoreGram.Data.Dtos;
using CoreGram.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreGram.Repositories
{
    public class PostRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly FollowerRepository _followerRepository;
        public PostRepository(DataContext context, IMapper mapper, FollowerRepository followerRepository)
        {
            _context = context;
            _mapper = mapper;
            _followerRepository = followerRepository;
        }

        public async Task<IEnumerable<PostDto>> GetAll()
        {

            var model = await _context.Posts
                .OrderByDescending(x => x.Date)
                .ToListAsync();

            return _mapper.Map<List<Post>, List<PostDto>>(model);
        }

        public async Task<IEnumerable<PostDto>> GetByUser(int userId)
        {
            var model = await _context.Posts.FindAsync(userId);
            if (model == null)
            {
                throw new Exception("No se ha encontrado el usuario");
            }

            var result = await _context.Posts.Where(x => x.UserId == userId)
                .OrderByDescending(x => x.Date)
                .ToListAsync();

            return _mapper.Map<List<Post>, List<PostDto>>(result);
        }

        public async Task<IEnumerable<PostDto>> GetByFollowings(int userId)
        {
            var followersIds = _followerRepository.GetFollowings(userId).Select(x => x.UserId).ToArray();

            var result = await _context.Posts.Where(x => followersIds.Contains(x.UserId))
                .OrderByDescending(x => x.Date)
                .ToListAsync();

            return _mapper.Map<List<Post>, List<PostDto>>(result);
        }

        public async Task<PostDto> Create(PostDto dto)
        {
            var model = _mapper.Map<Post>(dto);
            _context.Posts.Add(model);
            await _context.SaveChangesAsync();
            return _mapper.Map<PostDto>(model);
        }

        public async Task<PostDto> Delete(int postId)
        {
            var model = await _context.Posts.FindAsync(postId);
            if (model == null)
            {
                throw new Exception("No se ha encontrado la publicación");
            }
            _context.Posts.Remove(model);
            await _context.SaveChangesAsync();
            return _mapper.Map<PostDto>(model);
        }
    }
}
