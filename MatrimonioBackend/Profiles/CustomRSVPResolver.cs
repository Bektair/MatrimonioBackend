using AutoMapper;
using AutoMapper.Configuration.Conventions;
using MatrimonioBackend.DTOs.RSVP;
using MatrimonioBackend.Models;

namespace MatrimonioBackend.Profiles
{


    public class CustomRSVPResolver : IValueResolver<RSVPCreateDTO, RSVP, DateTime>
    {
        public DateTime Resolve(RSVPCreateDTO source, RSVP destination, DateTime destMember, ResolutionContext context)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                .AddMilliseconds(source.Deadline)
                .ToLocalTime();

        }
    }
}
