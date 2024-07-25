namespace MatrimonioBackend.DTOs.Participant
{
    public class ParticipantTranslationCreateDTO
    {
        public string Role { get; set; }
        public string Language { get; set; }
        public bool IsDefaultLanguage { get; set; }
    }
}
