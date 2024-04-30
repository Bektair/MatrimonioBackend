using AutoMapper;
using MatrimonioBackend.DTOs.Reception;
using MatrimonioBackend.Models;

namespace MatrimonioBackend.Profiles
{
    public class ReceptionProfile : Profile
    {
        public ReceptionProfile()
        {
            CreateMap<Reception, ReceptionReadDTO>();
            CreateMap<ReceptionCreateDTO, Reception>();
        }

    }
}
