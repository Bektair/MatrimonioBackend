using MatrimonioBackend.Models;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrimonioBackend.Models
{
    public class Wedding
    {
        public int id { get; set; }
        public string description { get; set; }
        public string dresscode { get; set; }
        public string primaryColor { get; set; }
        public string secoundaryColor { get; set; }
        public string primaryFontColor { get; set; }
        public string secoundaryFontColor { get; set; }
        public string backgroundImage   { get; set; }
        public string bodyFont { get; set; }
        public string headingFont { get; set; }


        //NavProperties
        public Reception? Reception { get; set; } 
        public ReligiousCeremony? ReligiousCeremony{ get; set; }

        public ICollection<Participant> Participants { get; set; } = new List<Participant>();


        public ICollection<RSVP> RSVPs { get; set; } = new List<RSVP>();
        public ICollection<Post> Posts { get; set; } = new List<Post>();



    }
}
