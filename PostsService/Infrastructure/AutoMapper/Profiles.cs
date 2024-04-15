using AutoMapper;
using PostsService.Features.Dtos;
using PostsService.Features.Entities;

namespace PostsService.Infrastructure.AutoMapper;

public class Profiles : Profile
{
    public Profiles()
    {
        CreateMap<Post, PostDto>().ReverseMap();
        CreateMap<Post, PostFormUpdateDto>().ReverseMap();
        CreateMap<Post, PostFormDto>().ReverseMap()
                .ForMember(p => p.Tags, opt => opt.Ignore());       // Ignore Tag mapping to avoid EF complaining and setting the IDs


        CreateMap<Category, CategoryDto>().ReverseMap();
        CreateMap<Category, CategoryFormDto>().ReverseMap();


        CreateMap<Tag, TagDto>().ReverseMap();
        CreateMap<Tag, TagFormDto>().ReverseMap();
    }
}
