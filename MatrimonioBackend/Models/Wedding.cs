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
        public int Id { get; set; }
        public string Description { get; set; }
        public string Dresscode { get; set; }

        public string MainColor { get; set; }
        public string SecoundaryColor { get; set; }
        public string MainFontColor { get; set; }
        public string SecoundaryFontColor { get; set; }

        public string BackgroundImage   { get; set; }

        public string BodyFont { get; set; }
        public string HeadingFont { get; set; }


        //NavProperties
        public Reception? Reception { get; set; } 
        public ReligiousCeremony? ReligiousCeremony{ get; set; }

        public ICollection<Participant> Participants { get; set; } = new List<Participant>();


        public ICollection<RSVP> RSVPs { get; set; } = new List<RSVP>();
        public ICollection<Post> Posts { get; set; } = new List<Post>();



    }
}
