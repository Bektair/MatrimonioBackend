
namespace MatrimonioBackend.Models
{
    public class Participant
    {

        //Nav
        public Guid UserId { get; set; }
        public MarryMonioUser User { get; set; }

        public Wedding Wedding { get; set; }
        public int WeddingId { get; set; }

        public ICollection<ParticipantTranslation> Translations { get; set; } = new List<ParticipantTranslation>();

    }
}
