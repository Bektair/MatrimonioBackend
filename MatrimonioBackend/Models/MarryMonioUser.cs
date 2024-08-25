using MatrimonioBackend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace MatrimonioBackend.Models
{
    public class MarryMonioUser
    {
        public Guid Id { get; set; }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        
        public string? ProfilePicture { get; set; }

        public string Email { get; set; }

        public string? Nickname { get; set; }
        public string? Password { get; set; }
        public string? Language { get; set; }

        public bool Email_Verified { get; set; }
        public bool IsSocialAccount { get; set; } = false;

        //Nav properties
        public ICollection<Participant> Participants { get; set; } = new List<Participant>();

        public ICollection<RSVP> RSVPs { get; set; } = new List<RSVP>();
        public ICollection<Post> Posts { get; set; } = new List<Post>();


    }
}
