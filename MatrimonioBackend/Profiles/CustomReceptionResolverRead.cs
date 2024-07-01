using AutoMapper;
using MatrimonioBackend.DTOs.Reception;
using MatrimonioBackend.DTOs.RSVP;
using MatrimonioBackend.Models;

namespace MatrimonioBackend.Profiles
{
    public class CustomReceptionResolverRead : IValueResolver<Reception, ReceptionReadDTO, ICollection<MenuOptionReadDTO>>
    {
        public ICollection<MenuOptionReadDTO> Resolve(Reception source, ReceptionReadDTO destination, ICollection<MenuOptionReadDTO> destMember, ResolutionContext context)
        {
            var MenuOptions = new List<MenuOptionReadDTO>();
            foreach (var item in source.MenuOptions)
            {
                var menuOption = new MenuOptionReadDTO()
                {
                    DishName = item.DishType,
                    Id = item.Id,
                    Image = item.Image,
                    Tags = item.Tags,
                };
                MenuOptions.Add(menuOption);
            }
            return MenuOptions;
        }
    }


}

