using MatrimonioBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrimonioBackend.Models
{
    public class Post
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }

        //Nav properties
        public User Author { get; set; }
        public int AuthorId { get; set; }
        public Wedding Wedding { get; set; }
        public int WeddingId { get; set; }

        public ICollection<PostImage> images { get; set; }

    }
}
