using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrimonioBackend.Models
{
    public class Location
    {
        public int Id { get; set; }


        public string? Image {  get; set; }

        public ICollection<LocationTranslation> Translations { get; set; } = new List<LocationTranslation>();
        ////Nav properties
        public ICollection<Reception>? Receptions { get; set; }
        public ICollection<ReligiousCeremony>? ReligiousCeremonies { get; set; }

    }
}
