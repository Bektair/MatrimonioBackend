using MatrimonioBackend.Models;

namespace MatrimonioBackend.DTOs.Participant
{
    public class ParticipantCreateDTO
    {
        //Nav
        public string Role { get; set; }
        public string Language { get; set; }
        public bool IsDefaultLanguage { get; set; }
        public int WeddingId { get; set; }
        public Guid UserId { get; set; }

    }
}
