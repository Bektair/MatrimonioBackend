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
        public string Title { get; set; }
        //299 Rue le Carr, Fermont, QC G0G 1J0, Canada Fra googlemaps format.

        public string Country { get; set; }
        public string Region { get; set; }
        public string Placename { get; set; }
        public string Address { get; set; }
        public string Body { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }

        public string? Image {  get; set; }



        ////Nav properties
        public ICollection<Reception>? Receptions { get; set; }
        public ICollection<ReligiousCeremony>? ReligiousCeremonies { get; set; }

    }
}
