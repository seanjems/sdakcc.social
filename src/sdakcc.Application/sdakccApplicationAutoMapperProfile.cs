using AutoMapper;
using sdakcc.Entities;

namespace sdakcc;

public class sdakccApplicationAutoMapperProfile : Profile
{
    public sdakccApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<PostsDto.CreatePostDto, Posts>();
    }
}
