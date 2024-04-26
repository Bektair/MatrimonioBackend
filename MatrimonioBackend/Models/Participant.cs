using MySqlManager.Models;

namespace MatrimonioBackend.Models
{
    public class Participant
    {
        public string Role { get; set; }

        //Nav
        public int UserId { get; set; }
        public User User { get; set; }

        public Wedding Wedding { get; set; }
        public int WeddingId { get; set; }
    }
}
