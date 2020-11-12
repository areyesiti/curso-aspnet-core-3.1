using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreGram.Data.Models
{
    public class Follower
    {
        public int UserId { get; set; }
        public int FollowerId { get; set; }
        public DateTime Date { get; set; }
        public virtual User UserFollower { get; set; }
        public virtual User UserFollowing { get; set; }
    }
}
