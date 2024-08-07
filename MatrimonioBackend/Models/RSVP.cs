﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrimonioBackend.Models
{
    public class RSVP
    {
        public int Id { get; set; }
        public DateTime Deadline { get; set; }

        public int NumberOfGuests { get; set; }
        public string OtherDietaryRequirements { get; set; }
        public string Status { get; set; }


        //Nav
        public IEnumerable<MenuOrder> MenuOrders { get; set; }

        public Wedding Wedding { get; set; }
        public int WeddingId { get; set; }

        public MarryMonioUser Signer { get; set; }
        public Guid SignerId { get; set; }

        public ICollection<RSVPTranslation> Translations { get; set; } = new List<RSVPTranslation>();


    }
}
