
namespace MatrimonioBackend.Models
{
    public class PostImage
    {
        public int Id{ get; set; }

        public string URI { get; set; }

        public string? Size { get; set; }

        public string? Role { get; set; }

        //Nav
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
