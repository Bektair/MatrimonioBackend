using AutoMapper;
using MatrimonioBackend.DTOs.Location;
using MatrimonioBackend.DTOs.Reception;
using MatrimonioBackend.DTOs.RSVP;
using MatrimonioBackend.Models;

namespace MatrimonioBackend.Profiles
{
    public class CustomReceptionResolverCreate : ITypeConverter<ReceptionCreateDTO, Reception>
    {
        public Reception Convert(ReceptionCreateDTO source, Reception destination, ResolutionContext context)
        {
            var translation = new ReceptionTranslation
            {
                Description = source.Description,
                IsDefaultLanguage = source.IsDefaultLanguage,
                Language = source.Language
            };


            var resception = new Reception
            {
                EndDate = source.EndDate,
                StartDate = source.StartDate,
                LocationId = source.LocationId,
                WeddingId = source.WeddingId,
                Translations = new List<ReceptionTranslation>() { translation },
            };
            return resception;
        }
    }

    public class CustomReceptionMenuOptionResolverCreate : ITypeConverter<MenuOptionCreateDTO, MenuOption>
    {
        public MenuOption Convert(MenuOptionCreateDTO source, MenuOption destination, ResolutionContext context)
        {
            var translation = new MenuOptionTranslation
            {
                DishType = source.DishType,
                Tags = source.Tags,
                IsDefaultLanguage = source.IsDefaultLanguage,
                Language = source.Language
            };


            var menuOption = new MenuOption
            {
                Image = source.Image,
                Translations = new List<MenuOptionTranslation>() { translation },
            };
            return menuOption;
        }
    }


}

