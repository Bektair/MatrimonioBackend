using AutoMapper;
using MatrimonioBackend.DTOs.Reception;
using MatrimonioBackend.Models;

namespace MatrimonioBackend.Profiles
{
    public class ReceptionProfile : Profile
    {
        public ReceptionProfile()
        {
            CreateMap<Reception, ReceptionReadDTO>().ForMember(dest => dest.MenuOptions, opt => opt.MapFrom<CustomReceptionResolverRead>());
            CreateMap<ReceptionCreateDTO, Reception>();
        }

    }
}
