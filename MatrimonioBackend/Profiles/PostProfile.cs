using AutoMapper;
using MatrimonioBackend.DTOs.Post;
using MatrimonioBackend.Models;

namespace MatrimonioBackend.Profiles
{
    public class PostProfile : Profile
    {

        public PostProfile()
        {
            CreateMap<PostCreateDTO, Post>();
            CreateMap<Post, PostReadDTO>();

        }
    }
}
