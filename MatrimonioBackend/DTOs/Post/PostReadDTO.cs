using MatrimonioBackend.Models;

namespace MatrimonioBackend.DTOs.Post
{
    public class PostReadDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public Guid AuthorId { get; set; }
        public int WeddingId { get; set; }

    }
}
