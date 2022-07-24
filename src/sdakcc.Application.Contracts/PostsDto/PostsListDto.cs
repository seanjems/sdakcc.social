using System;
using System.Collections.Generic;

namespace sdakcc.PostsDto
{

    public class PostsListDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Description { get; set; }
        
        public string Image { get; set; }
        public List<PostLikes> PostLikes { get; set; }

      

    }
}
