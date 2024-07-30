using AutoMapper;
using AutoMapper.Configuration.Conventions;
using MatrimonioBackend.DTOs.RSVP;
using MatrimonioBackend.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace MatrimonioBackend.Profiles
{
    public class CustomRSVPResolverCreate : ITypeConverter<RSVPCreateDTO, RSVP>
    {
        public RSVP Convert(RSVPCreateDTO source, RSVP destination, ResolutionContext context)
        {
            var rsvpTranslate = new List<RSVPTranslation>() {new RSVPTranslation()
            {
                Body = source.Body,
                Language = source.Language,
                IsDefaultLanguage = true,
            } };

            var rsvp = new RSVP()
            {
                Deadline = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(source.Deadline).ToLocalTime(),
                Translations = rsvpTranslate,
                Status = source.Status,
                OtherDietaryRequirements = source.OtherDietaryRequirements,
                NumberOfGuests = source.NumberOfGuests,
                SignerId = source.SignerId,
                WeddingId = source.WeddingId,
            };
            return rsvp;

        }
    }
}

