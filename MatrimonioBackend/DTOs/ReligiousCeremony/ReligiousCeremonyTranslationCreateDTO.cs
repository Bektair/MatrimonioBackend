namespace MatrimonioBackend.DTOs.ReligiousCeremony
{
    public class ReligiousCeremonyTranslationCreateDTO
    {
        public string Language { get; set; }
        public bool IsDefaultLanguage { get; set; }
        public string Description { get; set; }
    }
}
