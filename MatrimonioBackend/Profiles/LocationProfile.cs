using AutoMapper;
using MatrimonioBackend.DTOs.Location;
using MatrimonioBackend.Models;

namespace MatrimonioBackend.Profiles
{
    public class LocationProfile : Profile
    {
        public LocationProfile()
        {
            CreateMap<LocationCreateDTO, Location>();
            CreateMap<Location, LocationReadDTO>();


        }

    }
}
