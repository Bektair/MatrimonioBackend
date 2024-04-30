using MatrimonioBackend.DAL;
using MatrimonioBackend.Models;
using System;

namespace ContosoUniversity.DAL
{
    public class UnitOfWork : IDisposable
    {
        private WeddingContext context = new WeddingContext();
        private GenericRepository<User> _userRepository;
        private GenericRepository<Wedding> _weddingRepository;
        private GenericRepository<Participant> _participantRepository;
        private GenericRepository<RSVP> _RSVPRepository;
        private GenericRepository<Post> _postRepository;
        private GenericRepository<Reception> _receptionRepository;
        private GenericRepository<ReligiousCeremony> _religiousCeremonyRepository;
        private GenericRepository<Location> _locationRepository;

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

        public GenericRepository<RSVP> RSVPRepository
        {
            get
            {

                if (this._RSVPRepository == null)
                {
                    this._RSVPRepository = new GenericRepository<RSVP>(context);
                }
                return _RSVPRepository;
            }
        }

        public GenericRepository<Post> PostRepository
        {
            get
            {

                if (this._postRepository == null)
                {
                    this._postRepository = new GenericRepository<Post>(context);
                }
                return _postRepository;
            }
        }

        public GenericRepository<Reception> ReceptionRepository
        {
            get
            {

                if (this._receptionRepository == null)
                {
                    this._receptionRepository = new GenericRepository<Reception>(context);
                }
                return _receptionRepository;
            }
        }

        public GenericRepository<ReligiousCeremony> ReligiousCeremonyRepository
        {
            get
            {

                if (this._religiousCeremonyRepository == null)
                {
                    this._religiousCeremonyRepository = new GenericRepository<ReligiousCeremony>(context);
                }
                return _religiousCeremonyRepository;
            }
        }

        public GenericRepository<Location> LocationRepository
        {
            get
            {

                if (this._locationRepository == null)
                {
                    this._locationRepository = new GenericRepository<Location>(context);
                }
                return _locationRepository;
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