using Swashbuckle.AspNetCore.SwaggerGen;

namespace MatrimonioBackend.Models.Constants
{
    public static class RSVPStatus
    {
        public const string Pending =  nameof(Pending);
        public const string Accepted = nameof(Accepted);
        public const string Declined =  nameof(Declined);
        public const string PastDeadline =  nameof(PastDeadline);
        public const string AcceptedCeremony =  nameof(AcceptedCeremony);

    }
}
