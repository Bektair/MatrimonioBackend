using AutoMapper;
using MatrimonioBackend.DTOs.RSVP;
using MatrimonioBackend.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace MatrimonioBackend.Profiles
{
    public class RSVPProfile : Profile
    {

        public RSVPProfile()
        {
         
            CreateMap<RSVPCreateDTO, RSVP>().ForMember(dest => dest.Deadline, opt => opt.MapFrom<CustomRSVPResolverCreate>());
            CreateMap<RSVPUpdateDTO, RSVP>();
            CreateMap<JsonPatchDocument<RSVPUpdateDTO>, JsonPatchDocument<RSVP>>().ConvertUsing(new CustomRSVPResolverUpdate());
            CreateMap<RSVP, RSVPReadDTO>();
            CreateMap<MenuOrder, MenuOrderReadDTO>();
            CreateMap<MenuOrderCreateDTO, MenuOrder>();

        }





    }
}
