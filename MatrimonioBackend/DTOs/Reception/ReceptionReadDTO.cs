using MatrimonioBackend.DTOs.Location;
using MatrimonioBackend.Models;

namespace MatrimonioBackend.DTOs.Reception
{
    public class ReceptionReadDTO
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string Description { get; set; }

        //Nav properties
        public LocationReadDTO Location{ get; set; }
        public int? WeddingId { get; set; }

        public ICollection<MenuOptionReadDTO> MenuOptions { get; set; } = new List<MenuOptionReadDTO>();

    }
}
