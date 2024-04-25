using MySqlManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySqlManager.Repository
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();

        User GetUserById(int user_id);
        bool InsertUser(User user);
        bool DeleteUser(int user_id);
        bool UpdateUser(User user);




    }
}
