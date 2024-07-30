namespace MatrimonioBackend.Models
{
    public class ReligiousCeremonyTranslation : ITranslation
    {
        public string Language { get; set; }
        public bool IsDefaultLanguage { get; set; }
        public string Description { get; set; }

        public int? ReligiousCeremonyId { get; set; }
        public ReligiousCeremony? ReligiousCeremony { get; set; }
    }
}
