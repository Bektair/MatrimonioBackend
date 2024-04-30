﻿using AutoMapper;
using MatrimonioBackend.DTOs.Wedding;
using MatrimonioBackend.Models;

namespace MatrimonioBackend.Profiles
{
    public class WeddingProfile : Profile
    {
        public WeddingProfile() { 
            CreateMap<WeddingCreateDTO, Wedding>();
            CreateMap<WeddingUpdateDTO, Wedding>();
            CreateMap<Wedding, WeddingGetDTO>();
        }

    }
}
