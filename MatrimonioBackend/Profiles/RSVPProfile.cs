using AutoMapper;
using MatrimonioBackend.DTOs.RSVP;
using MatrimonioBackend.Models;

namespace MatrimonioBackend.Profiles
{
    public class RSVPProfile : Profile
    {

        public RSVPProfile()
        {
         
            CreateMap<RSVPCreateDTO, RSVP>().ForMember(dest => dest.Deadline, opt => opt.MapFrom<CustomRSVPResolver>());

            CreateMap<RSVP, RSVPReadDTO>();

        }





    }
}
