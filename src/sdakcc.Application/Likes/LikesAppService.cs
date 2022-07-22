
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sdakcc.LikeseDto;
using sdakcc.PostsDto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace sdakcc.Application.Likes

{
    public class LikesAppService: sdakccAppService
    {
        private readonly IRepository<Entities.Like> _likesRepos;
        private readonly IRepository<Entities.Posts, Guid> _postsRepos;

        public LikesAppService(IRepository<Entities.Like> likesRepos, IRepository<Entities.Posts, Guid> postsRepos)
        {
            _likesRepos = likesRepos;
            _postsRepos = postsRepos;
        }

        //Create Post

        public async Task<ActionResult<PostsListDto>> LikeAsync(CreateLikeDto createLikeDto)
        {

            var outPut = ObjectMapper.Map<CreateLikeDto, Entities.Like>(createLikeDto);
            var objFromDb = await _likesRepos.FirstOrDefaultAsync(x => x.PostId == outPut.PostId && x.UserId == outPut.UserId);
            await CurrentUnitOfWork.SaveChangesAsync();
            if (objFromDb == null)
            {
                var createLike = await _likesRepos.InsertAsync(outPut);
                await CurrentUnitOfWork.SaveChangesAsync();
            }
            else
            {
                await _likesRepos.DeleteAsync(x => x.PostId == outPut.PostId && x.UserId == outPut.UserId);
                await CurrentUnitOfWork.SaveChangesAsync();
            }

            var result = await _postsRepos.GetAsync(createLikeDto.PostId, includeDetails :true);
            if (result == null) return null;
            return ObjectMapper.Map<Entities.Posts, PostsListDto>(result);

        }



    }
}
