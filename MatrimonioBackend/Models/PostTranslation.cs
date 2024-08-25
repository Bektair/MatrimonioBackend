namespace MatrimonioBackend.Models
{
    public class PostTranslation : ITranslation
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public string Language { get; set; }
        public bool IsDefaultLanguage { get; set; }

        public int? PostId { get; set; }
        public Post? Post { get; set; }
    }
}
