﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreGram.Data.Dtos
{
    public class LikeDto
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
    }
}
