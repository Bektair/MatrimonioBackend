﻿namespace MatrimonioBackend.DTOs.RSVP
{
    public class RSVPCreateDTO
    {
        public string Body { get; set; }
        public DateTime Deadline { get; set; }

        public string Status { get; set; }
        public int NumberOfGuests { get; set; }
        public string DietaryRequirements { get; set; }

    }
}
