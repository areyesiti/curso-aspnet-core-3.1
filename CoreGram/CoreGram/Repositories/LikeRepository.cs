using AutoMapper;
using CoreGram.Data;
using CoreGram.Data.Dtos;
using CoreGram.Data.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
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

        public async Task<int> GetByPost(int postId)
        {
            // return _context.Likes.Count(x => x.PostId == postId);

            var pTotal = new SqlParameter()
            {
                ParameterName = "@total",
                Direction = ParameterDirection.Output,
                Value = 0,
                DbType = DbType.Int32
            };

            var pPost = new SqlParameter()
            {
                ParameterName = "@postId",                
                Value = postId,
                DbType = DbType.String
            };

            _context.Database.ExecuteSqlCommand("sp_GetLikesByPost @postId, @total OUTPUT", pPost, pTotal);

            return Convert.ToInt32(pTotal.Value);
        }
    }
}
