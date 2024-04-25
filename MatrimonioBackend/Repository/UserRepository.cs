using MatrimonioBackend.DAL;
using Microsoft.EntityFrameworkCore;
using MySqlManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySqlManager.Repository
{
    public class UserRepository : IUserRepository, IDisposable
    {
        public WeddingContext context;

        public UserRepository(WeddingContext context)
        {
            this.context = context;
        }

        public IEnumerable<User> GetAll()
        {
            return context.User.ToList();
        }

        public User GetUserById(int user_id)
        {
            return context.User.Find(user_id);
        }

        public bool InsertUser(User user)
        {
            context.User.Add(user);
            return true;
        }
        public bool UpdateUser(User user)
        {
            context.Entry(user).State = EntityState.Modified;
            return true;
        }
        public bool DeleteUser(int user_id)
        {
            User user = context.User.Find(user_id);
            context.User.Remove(user);
            return true;
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
