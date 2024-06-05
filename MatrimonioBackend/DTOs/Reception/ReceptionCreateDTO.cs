using MatrimonioBackend.Models;

namespace MatrimonioBackend.DTOs.Reception
{
    public class ReceptionCreateDTO
    {
        public DateTime Date { get; set; }
        public string Description { get; set; }

        //Nav properties
        public int LocationId { get; set; }
        public int WeddingId { get; set; }

        public ICollection<MenuOption> MenuOptions { get; set; } = new List<MenuOption>();


    }
}
