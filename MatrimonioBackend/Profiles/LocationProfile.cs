using AutoMapper;
using MatrimonioBackend.DTOs.Location;
using MatrimonioBackend.DTOs.Wedding;
using MatrimonioBackend.Models;
using Microsoft.AspNetCore.JsonPatch;
using static MatrimonioBackend.Profiles.CustomLocationResolverUpdate;

namespace MatrimonioBackend.Profiles
{
    public class LocationProfile : Profile
    {
        public LocationProfile()
        {
            CreateMap<LocationCreateDTO, Location>().ConvertUsing(new CustomLocationResolverCreate());

            CreateMap<Location, LocationReadDTO>();
            CreateMap<LocationTranslationCreateDTO, LocationTranslation>();
            CreateMap<JsonPatchDocument<LocationUpdateDTO>, JsonPatchDocument<Location>>().ConvertUsing(new CustomLocationResolverMainUpdate());
            CreateMap<JsonPatchDocument<LocationUpdateDTO>, JsonPatchDocument<LocationTranslation>>().ConvertUsing(new CustomLocationTranslateResolverUpdate());
        }

    }
}
