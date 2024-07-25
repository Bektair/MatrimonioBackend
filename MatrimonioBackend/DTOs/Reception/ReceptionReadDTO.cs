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
        public bool IsDefaultLanguage { get; set; }
        public string Language { get; set; }

        //Nav properties
        public LocationReadDTO? Location{ get; set; }
        public int? WeddingId { get; set; }

        public IEnumerable<MenuOptionReadDTO>? MenuOptions { get; set; } = new List<MenuOptionReadDTO>();

    }
}
