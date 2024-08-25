using AutoMapper;
using MatrimonioBackend.DTOs.Post;
using MatrimonioBackend.DTOs.Wedding;
using MatrimonioBackend.Models;
using Microsoft.AspNetCore.JsonPatch;
using static MatrimonioBackend.Profiles.CustomPostResolverCreate;

namespace MatrimonioBackend.Profiles
{
    public class PostProfile : Profile
    {

        public PostProfile()
        {
            CreateMap<PostCreateDTO, Post>().ConvertUsing(new CustomPostResolverCreatePost());
            CreateMap<Post, PostReadDTO>();
            CreateMap<PostImage, PostImageReadDTO>();
            CreateMap<PostTranslationCreateDTO,  PostTranslation>().ConvertUsing(new CustomPostResolverTranslateCreate());
            CreateMap<JsonPatchDocument<PostUpdateDTO>, JsonPatchDocument<Post>>().ConvertUsing(new CustomPostResolverUpdatePost());
            CreateMap<JsonPatchDocument<PostUpdateDTO>, JsonPatchDocument<PostTranslation>>().ConvertUsing(new CustomPostResolverUpdateTranslatePost());

        }
    }
}
