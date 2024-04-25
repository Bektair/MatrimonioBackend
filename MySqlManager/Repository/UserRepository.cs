using MySqlManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySqlManager.Repository
{
    public class UserRepository : IUserRepository
    {
        public bool DeleteUser(int user_id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public User GetUserById(int user_id)
        {
            throw new NotImplementedException();
        }

        public bool InsertUser(User user)
        {
            throw new NotImplementedException();
        }

        public bool UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
