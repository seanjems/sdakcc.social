
using sdakcc.PostsDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace sdakcc.Application.Posts
{
    public class PostsAppService: sdakccAppService
    {
        private readonly IRepository<Entities.Posts, Guid> _postsRepos;

        public PostsAppService(IRepository<Entities.Posts, Guid> postsRepos)
        {
            _postsRepos = postsRepos;
        }

        //Create Post

        public async Task<CreatedPostOutDto> CreateAsync(CreatePostDto createPostDto)
        {
            var outPut = ObjectMapper.Map<CreatePostDto, Entities.Posts>(createPostDto);
            var createPosts = await _postsRepos.InsertAsync(outPut);

            return ObjectMapper.Map<Entities.Posts, CreatedPostOutDto>(createPosts);
        } 
    }
}
