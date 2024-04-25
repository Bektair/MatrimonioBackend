using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySqlManager.Models
{
    public class Location
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public double Ne_lat { get; set; }
        public double Ne_lng { get; set; }
        public double Sw_lat { get; set; }
        public double Sw_lng { get; set; }


        ////Nav properties
        public ICollection<Reception>? Receptions { get; set; }
        public ICollection<ReligiousCeremony>? ReligiousCeremonies { get; set; }

    }
}
