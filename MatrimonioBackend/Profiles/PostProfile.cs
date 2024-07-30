using AutoMapper;
using MatrimonioBackend.DTOs.Post;
using MatrimonioBackend.Models;
using static MatrimonioBackend.Profiles.CustomPostResolverCreate;

namespace MatrimonioBackend.Profiles
{
    public class PostProfile : Profile
    {

        public PostProfile()
        {
            CreateMap<PostCreateDTO, Post>();
            CreateMap<Post, PostReadDTO>();
            CreateMap<PostImage, PostImageReadDTO>();
            CreateMap<PostTranslationCreateDTO,  PostTranslation>().ConvertUsing(new CustomPostResolverTranslateCreate());

        }
    }
}
