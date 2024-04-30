using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySqlManager.Models
{
    public class Reception
    {
        public int Id { get; set; }
        public DateTime Date{ get; set; }
        public string Description { get; set; }

        ////Nav properties
        //public int LocationId{ get; set; }
        //public Location Location { get; set; }


    }
}
