using AutoMapper;
using MatrimonioBackend.DTOs.User;
using MySqlManager.Models;

namespace MatrimonioBackend.Profiles
{
    public class UserProfile : Profile
    {

        public UserProfile() {
            CreateMap<UserCreateDTO, User>();
            CreateMap<User, UserGetDTO>();


        }


    }
}
