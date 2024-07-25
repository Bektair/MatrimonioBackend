namespace MatrimonioBackend.DTOs.Location
{
    public class LocationTranslationCreateDTO
    {
        public string Country { get; set; }
        public string Region { get; set; }
        public string Placename { get; set; }
        public string Address { get; set; }

        public string Title { get; set; }
        public string Body { get; set; }

        public string Language { get; set; }
        public bool IsDefaultLanguage { get; set; }
    }
}
