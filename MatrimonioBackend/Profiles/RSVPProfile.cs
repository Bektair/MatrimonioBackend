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
            CreateMap<RSVPCreateDTO, RSVP>().ConvertUsing(new CustomRSVPResolverCreate());
            CreateMap<RSVPUpdateDTO, RSVP>();
            CreateMap<JsonPatchDocument<RSVPUpdateDTO>, JsonPatchDocument<RSVP>>().ConvertUsing(new CustomRSVPResolverUpdate());
            CreateMap<RSVP, RSVPReadDTO>();
            CreateMap<RSVPTranslationCreateDTO, RSVPTranslation>();
            CreateMap<MenuOrder, MenuOrderReadDTO>();
            CreateMap<MenuOrderCreateDTO, MenuOrder>();
        }





    }
}
