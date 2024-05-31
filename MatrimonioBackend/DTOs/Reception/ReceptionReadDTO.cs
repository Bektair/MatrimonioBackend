using MatrimonioBackend.DTOs.Location;

namespace MatrimonioBackend.DTOs.Reception
{
    public class ReceptionReadDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }

        //Nav properties
        public LocationReadDTO Location{ get; set; }
        public int? WeddingId { get; set; }
    }
}
