
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace sdakcc.LikeseDto
{
    
    public class CreateLikeDto
    {
        public Guid PostId { get; set; }
        public Guid UserId { get; set; }

    }
}
