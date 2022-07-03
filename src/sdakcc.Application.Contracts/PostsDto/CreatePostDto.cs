using Abp.AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace sdakcc.PostsDto
{
    
    public class CreatePostDto
    {
        public Guid UserId { get; set; }
        public string Description { get; set; }
        public IFormFile ImageName { get; set; }
      
    }
}
