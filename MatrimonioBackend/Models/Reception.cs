using MatrimonioBackend.DTOs.Reception;
using MatrimonioBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrimonioBackend.Models
{
    public class Reception
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        //Nav properties
        public int LocationId { get; set; }
        public Location Location { get; set; }

        public Wedding? Wedding { get; set; }
        public int? WeddingId { get; set; }

        public ICollection<ReceptionTranslation> Translations { get; set; } = new List<ReceptionTranslation>();
        public ICollection<MenuOption> MenuOptions { get; set; } = new List<MenuOption>();


    }
}
