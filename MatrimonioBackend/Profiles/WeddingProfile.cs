using AutoMapper;
using MatrimonioBackend.DTOs.RSVP;
using MatrimonioBackend.DTOs.Wedding;
using MatrimonioBackend.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace MatrimonioBackend.Profiles
{
    public class WeddingProfile : Profile
    {
        public WeddingProfile() { 
            CreateMap<WeddingCreateDTO, Wedding>().ConvertUsing(new CustomWeddingResolverCreate());
            CreateMap<WeddingTranslationCreateDTO, WeddingTranslation>();
            CreateMap<WeddingTranslation, WeddingTranslationReadDTO>();
            CreateMap<WeddingUpdateDTO, Wedding>();
            CreateMap<Wedding, WeddingReadDTO>();
            CreateMap<JsonPatchDocument<WeddingUpdateDTO>, JsonPatchDocument<Wedding>>().ConvertUsing(new CustomWeddingResolverUpdate());
            CreateMap<JsonPatchDocument<WeddingUpdateDTO>, JsonPatchDocument<WeddingTranslation>>().ConvertUsing(new CustomWeddingTranslateResolverUpdate());

        }

    }
}
