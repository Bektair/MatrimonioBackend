namespace MatrimonioBackend.DTOs.Post
{
    public class PostCreateDTO
    {
        public string Title { get; set; }
        public string Body { get; set; }

        public int AuthorId { get; set; }
        public int WeddingId { get; set; }



    }
}
