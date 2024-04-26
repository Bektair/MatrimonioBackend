using MatrimonioBackend.DAL;
using MatrimonioBackend.Models;
using MySqlManager.Models;
using System;

namespace ContosoUniversity.DAL
{
    public class UnitOfWork : IDisposable
    {
        private WeddingContext context = new WeddingContext();
        private GenericRepository<User> _userRepository;
        private GenericRepository<Wedding> _weddingRepository;
        private GenericRepository<Participant> _participantRepository;

        public GenericRepository<User> UserRepository
        {
            get
            {

                if (this._userRepository == null)
                {
                    this._userRepository = new GenericRepository<User>(context);
                }
                return _userRepository;
            }
        }

        public GenericRepository<Wedding> WeddingRepository
        {
            get
            {

                if (this._weddingRepository == null)
                {
                    this._weddingRepository = new GenericRepository<Wedding>(context);
                }
                return _weddingRepository;
            }
        }

        public GenericRepository<Participant> ParticipantRepository
        {
            get
            {

                if (this._participantRepository == null)
                {
                    this._participantRepository = new GenericRepository<Participant>(context);
                }
                return _participantRepository;
            }
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