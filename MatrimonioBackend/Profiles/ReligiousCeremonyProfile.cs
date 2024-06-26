﻿using AutoMapper;
using MatrimonioBackend.DTOs.ReligiousCeremony;
using MatrimonioBackend.Models;

namespace MatrimonioBackend.Profiles
{
    public class ReligiousCeremonyProfile : Profile
    {

        public ReligiousCeremonyProfile()
        {
            CreateMap<ReligiousCeremonyCreateDTO, ReligiousCeremony>();
            CreateMap<ReligiousCeremony, ReligiousCeremonyReadDTO>();



        }
    }
}
