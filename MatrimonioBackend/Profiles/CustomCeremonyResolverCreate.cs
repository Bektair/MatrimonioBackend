using AutoMapper;
using MatrimonioBackend.DTOs.Location;
using MatrimonioBackend.DTOs.ReligiousCeremony;
using MatrimonioBackend.Models;

namespace MatrimonioBackend.Profiles
{
    public class CustomCeremonyResolverCreate
    : ITypeConverter<ReligiousCeremonyCreateDTO, ReligiousCeremony>
    {

        public ReligiousCeremony Convert(ReligiousCeremonyCreateDTO source, ReligiousCeremony destination, ResolutionContext context)
        {
            var translations = new List<ReligiousCeremonyTranslation>
                {
                    new ReligiousCeremonyTranslation
                    {
                        IsDefaultLanguage = source.IsDefaultLanguage,
                        Language = source.Language,
                        Description= source.Description,
                    }
                };

            var rc = new ReligiousCeremony
            {
                EndDate = source.EndDate,
                StartDate = source.StartDate,
                LocationId = source.LocationId,
                WeddingId = source.WeddingId,
                Translations = translations,
            };

            return rc;
        }
    }

}
