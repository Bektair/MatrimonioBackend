namespace MatrimonioBackend.DTOs.Post
{
    public class PostTranslationCreateDTO
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public string Language { get; set; }
        public bool IsDefaultLanguage { get; set; }
    }
}
