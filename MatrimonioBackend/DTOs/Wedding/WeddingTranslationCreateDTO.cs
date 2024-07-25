namespace MatrimonioBackend.DTOs.Wedding
{
    public class WeddingTranslationCreateDTO
    {
        public string language { get; set; }
        public bool isDefaultLanguage { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string dresscode { get; set; }
    }
}
