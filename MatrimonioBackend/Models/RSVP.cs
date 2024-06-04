using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrimonioBackend.Models
{
    public class RSVP
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public DateTime Deadline { get; set; }

        public string Status { get; set; }
        public int NumberOfGuests { get; set; }
        public string DietaryRequirements { get; set; }

        //Nav
        public Wedding Wedding { get; set; }
        public int WeddingId { get; set; }

        public MarryMonioUser Signer { get; set; }
        public Guid SignerId { get; set; }



    }
}
