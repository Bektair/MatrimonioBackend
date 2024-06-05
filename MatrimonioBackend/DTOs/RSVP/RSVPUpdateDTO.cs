using MatrimonioBackend.Models;

namespace MatrimonioBackend.DTOs.RSVP
{
    public class RSVPUpdateDTO
    {
        public string Body { get; set; }
        public DateTime Deadline { get; set; }

        public string Status { get; set; }
        public int NumberOfGuests { get; set; }
        public string OtherDietaryRequirements { get; set; }

        public int? ChoosenDinnerId { get; set; }

        public int? ChoosenDessertId { get; set; }

        public Guid SignerId { get; set; }


    }
}
