
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace sdakcc.PostsDto
{
    
    public class CreatedPostOutDto
    {
        public Guid Id { get; set; }
        public Guid CreatorId { get; set; }
        public string Description { get; set; }
        public int PostType { get; set; }
        public string Image { get; set; }

    }
}
