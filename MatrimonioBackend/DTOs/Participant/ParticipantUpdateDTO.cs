namespace MatrimonioBackend.DTOs.Participant
{
    public class ParticipantUpdateDTO
    {

        public string Role { get; set; }

        //Nav
        public int UserId { get; set; }
        public int WeddingId { get; set; } 

    }
}
