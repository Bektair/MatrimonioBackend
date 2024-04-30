using MatrimonioBackend.Models;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace MatrimonioBackend.DAL
{
    public class WeddingContext : DbContext
    {
        public DbSet<Location> Location { get; set; }
        public DbSet<Reception> Reception { get; set; }
        public DbSet<ReligiousCeremony> ReligiousCeremony { get; set; }
        public DbSet<Wedding> Wedding { get; set; }
        public DbSet<RSVP> RSVP { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Post> Post { get; set; }
        public DbSet<Participant> Participant { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            optionsBuilder.UseNpgsql(Environment.GetEnvironmentVariable("DATA_SOURCE"));
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

            modelBuilder.Entity<User>()
                .HasMany(e => e.Posts)
                .WithOne(e => e.Author)
                .HasForeignKey(e => e.AuthorId)
                .IsRequired();


            modelBuilder.Entity<Post>()
                .HasMany(e => e.images)
                .WithOne(e => e.Post)
                .HasForeignKey(e => e.PostId)
                .IsRequired();



            seedData(modelBuilder);

            base.OnModelCreating(modelBuilder);

        }

        public ModelBuilder seedData(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>()
                .HasData(new User { Id = 1, FirstName = "Johnny", LastName="Depp", Keycloakid = "12345678-1234-1234-1234-123412341234", Email = "123@gmail.com" });
            modelBuilder.Entity<User>()
                .HasData(new User { Id = 2, FirstName = "Amber", LastName="Heard", Keycloakid = "12345678-1234-1234-1234-123412341234", Email = "123@gmail.com" });

            modelBuilder.Entity<User>()
                .HasData(new User { Id = 3, FirstName = "Ola", LastName = "Nordmann", Keycloakid = "12345678-1234-1234-1234-123412341234", Email = "123@gmail.com" });
            modelBuilder.Entity<User>()
                .HasData(new User { Id = 4, FirstName = "Kari", LastName = "Larsen", Keycloakid = "12345678-1234-1234-1234-123412341234", Email = "123@gmail.com" });

            modelBuilder.Entity<User>()
             .HasData(new User { Id = 5, FirstName = "Piler", LastName = "Heard", Keycloakid = "12345678-1234-1234-1234-123412341234", Email = "123@gmail.com" });
            modelBuilder.Entity<User>()
             .HasData(new User { Id = 6, FirstName = "Lise", LastName = "Larsen", Keycloakid = "12345678-1234-1234-1234-123412341234", Email = "123@gmail.com" });



            modelBuilder.Entity<Wedding>()
                .HasData(new Models.Wedding { Id = 1, Description = "We will be together for all time!", Dresscode = "FineClothing"});
            modelBuilder.Entity<Wedding>()
                .HasData(new Wedding { Id = 2, Description = "Samler hele familien for ett flott bryllup, vi skal være sammen for alltid", Dresscode = "Bunad" });

            modelBuilder.Entity<Participant>()
                .HasData(new Participant() { UserId = 1, WeddingId = 1, Role = "Husband" });
            modelBuilder.Entity<Participant>()
                .HasData(new Participant() { UserId = 2, WeddingId = 1, Role = "Wife" });

            modelBuilder.Entity<Participant>()
                .HasData(new Participant() { UserId = 3, WeddingId = 2, Role = "Ektemann" });
            modelBuilder.Entity<Participant>()
                .HasData(new Participant() { UserId = 4, WeddingId = 2, Role = "Kone" });

            modelBuilder.Entity<Participant>()
                .HasData(new Participant() { UserId = 5, WeddingId = 1, Role = "Guest" });
            modelBuilder.Entity<Participant>()
                .HasData(new Participant() { UserId = 6, WeddingId = 2, Role = "Gjest" });


            modelBuilder.Entity<Location>()
                .HasData(new Models.Location { Id = 1, Body = "Beautiful church inside a golf court", Title = "Amazing Church", Lat = 37.4, Lng = -115.1 });
            modelBuilder.Entity<Location>()
                .HasData(new Models.Location { Id = 2, Body = "Open-Air dancefield suitable for any occation", Title = "Central Park", Lat = 40.77, Lng = -73.97 });
            modelBuilder.Entity<Location>()
                .HasData(new Models.Location { Id = 3, Body = "Tradisjonsrik kirke, gjennoppreist etter samme design som middelalder kirken som brante ned i 1980-tallet", Title = "Nedre Bergslia Kirke", Lat = 63.2, Lng = 9.85 });
            modelBuilder.Entity<Location>()
                .HasData(new Models.Location { Id = 4, Body = "Store lokaler vel egnet for enhver avdeling", Title = "Grand Plaza Hotel", Lat = 59.92, Lng = 10.78 });

            modelBuilder.Entity<Reception>()
                .HasData(new Models.Reception { Id = 1, Date = DateTime.UtcNow.AddMonths(1), Description = "ONLY VIP, You need atleast 1million IMDB hits",
                    LocationId = 2, WeddingId = 1 });
                        modelBuilder.Entity<Reception>()
                .HasData(new Models.Reception { Id = 2, Date = DateTime.UtcNow.AddMonths(12), Description = "A professional and comfortable experience with great food",
                    LocationId = 4, WeddingId = 2 });

            modelBuilder.Entity<MenuOption>()
                .HasData(new MenuOption { Id = 1, DishName = "Russian Kaviar", Alergens = "None", ReceptionId = 1, Tags = "Fish", Image = "fi fi-sr-home" });
            modelBuilder.Entity<MenuOption>()
                .HasData(new MenuOption { Id = 2, DishName = "Sweltzer Salad", Alergens = "Peanuts", ReceptionId = 1, Tags = "Vegan", Image = "fi fi-sr-home" });
            modelBuilder.Entity<MenuOption>()
                .HasData(new MenuOption { Id = 3, DishName = "Ceasar Salad", Alergens = "Gluten", ReceptionId = 2, Tags = "Vegan", Image = "fi fi-sr-home" });
            modelBuilder.Entity<MenuOption>()
                .HasData(new MenuOption { Id = 4, DishName = "Tartare", Alergens = "", ReceptionId = 2, Tags = "Meat", Image = "fi fi-sr-home" });

            modelBuilder.Entity<ReligiousCeremony>()
                .HasData(new ReligiousCeremony { Id = 1, LocationId = 1, WeddingId = 1, Date = DateTime.UtcNow.AddMinutes(1), Description = "LAS VEGAS Baby, come and meet ous for the church!" });
            modelBuilder.Entity<ReligiousCeremony>()
                .HasData(new ReligiousCeremony { Id = 2, LocationId = 3, WeddingId = 2, Date = DateTime.UtcNow.AddMinutes(12), Description = "Kom til vår sermoni der vi sier våre løfter" });


            modelBuilder.Entity<Post>()
                .HasData(new Post() { Id = 1, AuthorId = 1, WeddingId = 1, Body = "Funky times, we will have loads of dance and party!!", Title = "PARTY!" });
            modelBuilder.Entity<Post>()
                .HasData(new Post() { Id = 2, AuthorId = 3, WeddingId = 2, Body = "Hei, vi ønsker oss disse gavene: - Penger, Gavekort", Title = "Gaveliste" });

            modelBuilder.Entity<PostImage>()
                .HasData(new PostImage() { Id = 1, PostId = 1, URI = "https://no.wikipedia.org/wiki/Johnny_Depp#/media/Fil:Johnny_Depp_2020.jpg", Role = "MainImage", Size = "245kb" });
            modelBuilder.Entity<PostImage>()
                .HasData(new PostImage() { Id = 2, PostId = 1, URI = "https://upload.wikimedia.org/wikipedia/commons/thumb/e/e3/Johnny_Depp%27s_signature.svg/512px-Johnny_Depp%27s_signature.svg.png", Role = "SideImage", Size = "15kb" });
            modelBuilder.Entity<PostImage>()
                .HasData(new PostImage() { Id = 3, PostId = 2, URI = "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d8/Vinmonopolets_logo.jpg/600px-Vinmonopolets_logo.jpg", Role = "SideImage", Size = "15kb" });
            modelBuilder.Entity<PostImage>()
                .HasData(new PostImage() { Id = 4, PostId = 2, URI = "https://upload.wikimedia.org/wikipedia/commons/9/9d/Red_Christmas_present_on_white_background.jpg", Role = "MainImage", Size = "155kb" });

            modelBuilder.Entity<RSVP> ()
                .HasData(new RSVP() { Id = 1, WeddingId = 1, Body = "Come to my wedding, there will be free alcohol", SignerId = 5, Deadline=DateTime.UtcNow.AddDays(14), NumberOfGuests=1, Status="Pending", DietaryRequirements="None"});
            modelBuilder.Entity<RSVP>()
               .HasData(new RSVP() { Id = 2, WeddingId = 1, Body = "Come to my wedding, there will be free alcohol", SignerId = 1, Deadline = DateTime.UtcNow.AddDays(14), NumberOfGuests = 1, Status = "Coming", DietaryRequirements = "None" });
            modelBuilder.Entity<RSVP>()
               .HasData(new RSVP() { Id = 3, WeddingId = 1, Body = "Come to my wedding, there will be free alcohol", SignerId = 2, Deadline = DateTime.UtcNow.AddDays(14), NumberOfGuests = 1, Status = "Coming", DietaryRequirements = "None" });
            modelBuilder.Entity<RSVP>()
                .HasData(new RSVP() { Id = 4, WeddingId = 2, Body = "Kom til det fantastiske brylluppet mitt", SignerId = 6, Deadline = DateTime.UtcNow.AddMonths(8), NumberOfGuests = 1, Status = "Avventer", DietaryRequirements = "None" });
            modelBuilder.Entity<RSVP>()
                .HasData(new RSVP() { Id = 5, WeddingId = 2, Body = "Kom til det fantastiske brylluppet mitt", SignerId = 3, Deadline = DateTime.UtcNow.AddMonths(8), NumberOfGuests = 1, Status = "Kommer", DietaryRequirements = "None" });
            modelBuilder.Entity<RSVP>()
                .HasData(new RSVP() { Id = 6, WeddingId = 2, Body = "Kom til det fantastiske brylluppet mitt", SignerId = 4, Deadline = DateTime.UtcNow.AddMonths(8), NumberOfGuests = 1, Status = "Kommer", DietaryRequirements = "None" });

            return modelBuilder;
        }

    }
}
