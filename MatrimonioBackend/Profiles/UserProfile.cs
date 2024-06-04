using AutoMapper;
using MatrimonioBackend.DTOs.User;
using MatrimonioBackend.Models;

namespace MatrimonioBackend.Profiles
{
    public class UserProfile : Profile
    {

        public UserProfile() {
            CreateMap<UserCreateDTO, MarryMonioUser>();
            CreateMap<MarryMonioUser, UserGetDTO>();


        }


    }
}
