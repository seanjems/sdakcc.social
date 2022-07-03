using Abp.AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace sdakcc.PostsDto
{
    
    public class CreateLikeDto
    {
        public Guid PostId { get; set; }
        public Guid UserId { get; set; }

    }
}
