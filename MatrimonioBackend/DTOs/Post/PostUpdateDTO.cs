namespace MatrimonioBackend.DTOs.Post
{
    public class PostUpdateDTO
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public Guid AuthorId { get; set; }
        public int WeddingId { get; set; }
        public string Language { get; set; }
        public bool IsDefaultLanguage { get; set; }

        public ICollection<PostImageReadDTO> Images { get; set; }
    }
}
