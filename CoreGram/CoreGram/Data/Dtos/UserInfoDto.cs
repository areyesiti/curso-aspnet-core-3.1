using CoreGram.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreGram.Data.Dtos
{
    public class UserInfoDto
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public UserProfileDto Profile { get; set; }
    }
}
