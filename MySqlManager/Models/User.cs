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

        public string username { get; set; }

        public string role { get; set; }

        public string keycloakid { get; set; }



    }
}
