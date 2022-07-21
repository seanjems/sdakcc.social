using AutoMapper;
using sdakcc.Entities;
using sdakcc.PostsDto;
using System.Collections.Generic;

namespace sdakcc;

public class sdakccApplicationAutoMapperProfile : Profile
{
    public sdakccApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<PostsDto.CreatePostDto, Posts>();
        CreateMap<Posts, PostsDto.CreatePostDto>();
        CreateMap<Posts, PostsDto.CreatedPostOutDto>();
        CreateMap<Entities.Posts, PostsListDto>();

    }
}
