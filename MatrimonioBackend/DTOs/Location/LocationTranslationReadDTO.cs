namespace MatrimonioBackend.DTOs.Location
{
    public class LocationTranslationReadDTO
    {
        public string Country { get; set; }
        public string Address { get; set; }

        public string Title { get; set; }
        public string Body { get; set; }

        public string Language { get; set; }
        public bool IsDefaultLanguage { get; set; }
    }
}
