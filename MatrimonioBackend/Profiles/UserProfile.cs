using AutoMapper;
using MatrimonioBackend.DTOs.User;
using MatrimonioBackend.DTOs.Wedding;
using MatrimonioBackend.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace MatrimonioBackend.Profiles
{
    public class UserProfile : Profile
    {

        public UserProfile() {
            CreateMap<UserCreateDTO, MarryMonioUser>();
            CreateMap<UserSocialCreateDTO, MarryMonioUser>();
            CreateMap<MarryMonioUser, UserGetDTO>();
            CreateMap<JsonPatchDocument<UserUpdateDTO>, JsonPatchDocument<MarryMonioUser>>()
                .ConvertUsing(new CustomUserRevolverUpdate());

        }


    }
}
