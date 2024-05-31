namespace MatrimonioBackend.DTOs.RSVP
{
    public class RSVPCreateDTO
    {

        public string Body { get; set; }
        public long Deadline { get; set; }

        public string Status { get; set; }
        public int NumberOfGuests { get; set; }
        public string DietaryRequirements { get; set; }

        public int SignerId { get; set; }
        public int WeddingId { get; set; }

    }
}
