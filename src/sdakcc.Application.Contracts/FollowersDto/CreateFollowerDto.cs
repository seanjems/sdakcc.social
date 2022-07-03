using Abp.AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace sdakcc.PostsDto
{
    
    public class CreatefolowerDto

    {
        public Guid UserId { get; set; }
        public Guid FollowerId { get; set; }       
    }
}
