using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CoreGram.Data.Dtos
{
    public class PostDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }        
        public string Photo { get; set; }
        public DateTime Date { get; set; }        
    }
}
