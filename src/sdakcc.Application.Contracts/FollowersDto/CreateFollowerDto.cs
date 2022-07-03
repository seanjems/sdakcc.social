
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace sdakcc.FollowersDto
{
    
    public class CreateFollowerDto

    {
        public Guid UserId { get; set; }
        public Guid FollowerId { get; set; }       
    }
}
