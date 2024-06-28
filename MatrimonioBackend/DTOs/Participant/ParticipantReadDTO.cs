namespace MatrimonioBackend.DTOs.Participant
{
    public class ParticipantReadDTO
    {
        public string Role { get; set; }

        public Guid UserId { get; set; }
        public int WeddingId { get; set; }



    }
}
