
using Newtonsoft.Json;

namespace MatrimonioBackend.DTOs.RSVP
{
    public class RSVPCreateDTO
    {

        public string Body { get; set; }
        public long Deadline { get; set; }

        public string Status { get; set; }
        public int NumberOfGuests { get; set; }
        public string OtherDietaryRequirements { get; set; }
        public Guid SignerId { get; set; }
        public int WeddingId { get; set; }

    }
}
