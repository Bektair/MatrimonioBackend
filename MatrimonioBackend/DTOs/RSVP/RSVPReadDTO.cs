using MatrimonioBackend.DTOs.User;

namespace MatrimonioBackend.DTOs.RSVP
{
    public class RSVPReadDTO
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public DateTime Deadline { get; set; }

        public string Status { get; set; }
        public int NumberOfGuests { get; set; }
        public string DietaryRequirements { get; set; }

        public UserGetDTO Signer { get; set; }

    }
}
