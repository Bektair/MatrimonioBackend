using MatrimonioBackend.Models;

namespace MatrimonioBackend.DTOs.Reception
{
    public class ReceptionCreateDTO
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string Description { get; set; }

        //Nav properties
        public int LocationId { get; set; }
        public int WeddingId { get; set; }

        public ICollection<MenuOptionReadDTO> MenuOptions { get; set; } = new List<MenuOptionReadDTO>();


    }
}
