﻿using MatrimonioBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySqlManager.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Keycloakid { get; set; }

        //Nav properties
        public ICollection<Participant> Participants { get; set; } = new List<Participant>();

        public ICollection<RSVP> RSVPs { get; set; } = new List<RSVP>();
        public ICollection<Post> Posts { get; set; } = new List<Post>();


    }
}
