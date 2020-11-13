using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreGram.Data.Models
{
    public class Like
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public virtual User User { get; set; }
        public virtual Post Post { get; set; }
    }
}
