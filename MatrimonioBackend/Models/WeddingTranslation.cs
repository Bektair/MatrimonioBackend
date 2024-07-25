namespace MatrimonioBackend.Models
{
    public class WeddingTranslation : ITranslation
    {
        public string Language { get; set; }
        public Wedding? Wedding { get; set; }
        public int? WeddingId { get; set; }
        public bool IsDefaultLanguage { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Dresscode { get; set; }


    }
}
