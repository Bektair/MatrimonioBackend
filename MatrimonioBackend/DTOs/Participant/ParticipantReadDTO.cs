namespace MatrimonioBackend.DTOs.Participant
{
    public class ParticipantReadDTO
    {
        public string Role { get; set; }

        public Guid UserId { get; set; }
        public int WeddingId { get; set; }
        public string Language { get; set; }
        public bool IsDefaultLanguage { get; set; }


    }
}
