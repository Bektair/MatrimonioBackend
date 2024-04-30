using AutoMapper;
using MatrimonioBackend.DTOs.RSVP;
using MatrimonioBackend.Models;

namespace MatrimonioBackend.Profiles
{
    public class RSVPProfile : Profile
    {

        public RSVPProfile()
        {
            CreateMap<RSVPCreateDTO, RSVP>();
            CreateMap<RSVP, RSVPReadDTO>();

        }





    }
}
