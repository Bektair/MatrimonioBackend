
namespace MatrimonioBackend.Models
{
    public class Participant
    {
        public string Role { get; set; }

        //Nav
        public Guid UserId { get; set; }
        public MarryMonioUser User { get; set; }

        public Wedding Wedding { get; set; }
        public int WeddingId { get; set; }
    }
}
