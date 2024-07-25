using MatrimonioBackend.Models;
using MatrimonioBackend.Models.Constants;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Reflection.Metadata;
using static System.Net.WebRequestMethods;

namespace MatrimonioBackend.DAL
{
    public class WeddingContext : DbContext
    {
        public DbSet<Location> Location { get; set; }
        public DbSet<Reception> Reception { get; set; }
        public DbSet<ReligiousCeremony> ReligiousCeremony { get; set; }
        public DbSet<Wedding> Wedding { get; set; }
        public DbSet<WeddingTranslation> WeddingTranslation { get; set; }
        public DbSet<RSVP> RSVP { get; set; }
        public DbSet<MarryMonioUser> MarryMonioUser { get; set; }
        public DbSet<Post> Post { get; set; }
        public DbSet<Participant> Participant { get; set; }
        public DbSet<MenuOrder> MenuOrders { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            optionsBuilder.UseNpgsql(Environment.GetEnvironmentVariable("DATA_SOURCE"))
                .LogTo(Console.WriteLine, LogLevel.Information);
            optionsBuilder.EnableSensitiveDataLogging();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Location>()
                .HasMany(e => e.Receptions)
                .WithOne(e => e.Location)
                .HasForeignKey(e => e.LocationId)
                .HasPrincipalKey(e => e.Id);
            modelBuilder.Entity<Location>()
                .HasMany(e => e.ReligiousCeremonies)
                .WithOne(e => e.Location)
                .HasForeignKey(e => e.LocationId)
                .HasPrincipalKey(e => e.Id);

            modelBuilder.Entity<Reception>()
                .HasOne(e => e.Wedding)
                .WithOne(e => e.Reception)
                .HasForeignKey<Reception>(e => e.WeddingId)
                .IsRequired(false);

            modelBuilder.Entity<Reception>()
                .HasMany(e => e.MenuOptions)
                .WithOne(e => e.Reception)
                .HasForeignKey(e => e.ReceptionId);


            modelBuilder.Entity<ReligiousCeremony>()
               .HasOne(e => e.Wedding)
               .WithOne(e => e.ReligiousCeremony)
               .HasForeignKey<ReligiousCeremony>(e => e.WeddingId)
               .IsRequired(false);

            modelBuilder.Entity<Participant>()
                .HasKey(uw => new { uw.WeddingId, uw.UserId });

            modelBuilder.Entity<Participant>()
                .HasOne(e => e.Wedding)
                .WithMany(e => e.Participants)
                .HasForeignKey(e => e.WeddingId);

            modelBuilder.Entity<Participant>()
                .HasOne(e => e.User)
                .WithMany(e => e.Participants)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<Wedding>()
                .HasMany(e => e.RSVPs)
                .WithOne(e => e.Wedding)
                .HasForeignKey(e => e.WeddingId)
                .IsRequired();
            
            modelBuilder.Entity<Wedding>()
                .HasMany(e => e.Posts)
                .WithOne(e => e.Wedding)
                .HasForeignKey(e => e.WeddingId)
                .IsRequired();

            modelBuilder.Entity<MarryMonioUser>()
                .HasMany(e => e.Posts)
                .WithOne(e => e.Author)
                .HasForeignKey(e => e.AuthorId)
                .IsRequired();
            modelBuilder.Entity<MarryMonioUser>().HasIndex(user => user.Email)
                .IsUnique();
            modelBuilder.Entity<MarryMonioUser>().Property(user => user.Id).HasDefaultValueSql("gen_random_uuid()"); //Postgres specific way of generating UUID v4

            modelBuilder.Entity<MarryMonioUser>().Property(user => user.Email_Verified).HasDefaultValue(false);

   

            modelBuilder.Entity<Post>()
                .HasMany(e => e.Images)
                .WithOne(e => e.Post)
                .HasForeignKey(e => e.PostId)
                .IsRequired();

            modelBuilder.Entity<WeddingTranslation>()
                .HasKey(uw => new { uw.WeddingId, uw.Language });


            modelBuilder.Entity<WeddingTranslation>()
                .HasOne(e => e.Wedding)
                .WithMany(e => e.Translations)
                .HasForeignKey(e => e.WeddingId)
                .IsRequired();

            modelBuilder.Entity<ParticipantTranslation>()
                .HasKey(parti => new { parti.Language, parti.WeddingId, parti.UserId });

            modelBuilder.Entity<LocationTranslation>()
              .HasOne(e => e.Location)
              .WithMany(e => e.Translations)
              .HasForeignKey(e => e.LocationId)
              .IsRequired();
            modelBuilder.Entity<LocationTranslation>()
                .HasKey(parti => new { parti.Language, parti.LocationId});
            modelBuilder.Entity<ReligiousCeremonyTranslation>()
                .HasKey(rc => new { rc.Language, rc.ReligiousCeremonyId });
            modelBuilder.Entity<ReceptionTranslation>()
                .HasKey(rc => new { rc.Language, rc.ReceptionId });
            modelBuilder.Entity<MenuOptionTranslation>()
                .HasKey(rc => new { rc.Language, rc.MenuOptionId });
            modelBuilder.Entity<RSVPTranslation>()
               .HasKey(rc => new { rc.Language, rc.RSVPId});
            modelBuilder.Entity<PostTranslation>()
               .HasKey(rc => new { rc.Language, rc.PostId});

            seedData(modelBuilder);

            base.OnModelCreating(modelBuilder);

        }
        public ModelBuilder seedData(ModelBuilder modelBuilder)
        {
            return WeddingSeed.seedModelBuilder(modelBuilder);
        }

    }
}
