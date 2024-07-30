using AutoMapper;
using MatrimonioBackend.DTOs.Reception;
using MatrimonioBackend.Models;

namespace MatrimonioBackend.Profiles
{
    public class ReceptionProfile : Profile
    {
        public ReceptionProfile()
        {
            CreateMap<Reception, ReceptionReadDTO>();
            CreateMap<ReceptionCreateDTO, Reception>().ConvertUsing(new CustomReceptionResolverCreate());
            CreateMap<MenuOption, MenuOptionReadDTO>();
            CreateMap<MenuOptionCreateDTO, MenuOption>().ConvertUsing(new CustomReceptionMenuOptionResolverCreate());
            CreateMap<ReceptionTranslationCreateDTO, ReceptionTranslation>().ConvertUsing(new CustomReceptionResolverTranslateCreate());
        }

    }
}
