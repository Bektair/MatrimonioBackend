using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrimonioBackend.Models
{
    public class Location
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }


        ////Nav properties
        public ICollection<Reception>? Receptions { get; set; }
        public ICollection<ReligiousCeremony>? ReligiousCeremonies { get; set; }

    }
}
