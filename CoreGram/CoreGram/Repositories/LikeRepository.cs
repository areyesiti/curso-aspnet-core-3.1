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
    public class LikeRepository
    {    
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public LikeRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<LikeDto> Like(LikeDto dto)
        {
            //var model = await _context.Likes.FirstOrDefaultAsync(x => x.UserId == dto.UserId && x.PostId == dto.PostId);

            var model = await _context.Likes.FromSqlRaw("SELECT * FROM Likes WHERE UserId = {0} AND PostId = {1}", dto.UserId, dto.PostId).FirstOrDefaultAsync();

            if (model != null)
            {
                _context.Likes.Remove(model);
            }
            else
            {
                model = _mapper.Map<Like>(dto);
                _context.Add(model);
            }

            _context.SaveChangesAsync();
            return _mapper.Map<LikeDto>(model);
        }
    }
}
