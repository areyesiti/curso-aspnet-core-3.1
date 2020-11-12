using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreGram.Data.Dtos
{
    public class FollowerDto
    {
        public int UserId { get; set; }
        public int FollowerId { get; set; }
        public DateTime Date { get; set; }
    }
}
