using AutoMapper;
using MatrimonioBackend.DTOs.Location;
using MatrimonioBackend.DTOs.Reception;
using MatrimonioBackend.DTOs.Wedding;
using MatrimonioBackend.Models;

namespace MatrimonioBackend.Profiles
{

    public class CustomLocationResolverCreate : ITypeConverter<LocationCreateDTO, Location>
    {

        public Location Convert(LocationCreateDTO source, Location destination, ResolutionContext context)
        {
            var translations = new List<LocationTranslation>
                {
                    new LocationTranslation
                    {
                        Address = source.Address,
                        Country = source.Country,
                        Body = source.Body,
                        IsDefaultLanguage = source.IsDefaultLanguage,
                        Language = source.Language,
                        Title = source.Title,
                    }
                };

            var location = new Location
            {
                Image = source.Image,
                Translations = translations,
            };

            return location;
        }
    }

}
