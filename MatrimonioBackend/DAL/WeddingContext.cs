using MatrimonioBackend.DTOs.Reception;
using MatrimonioBackend.Models;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
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
        public DbSet<RSVP> RSVP { get; set; }
        public DbSet<MarryMonioUser> MarryMonioUser { get; set; }
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
                .HasMany(e => e.images)
                .WithOne(e => e.Post)
                .HasForeignKey(e => e.PostId)
                .IsRequired();

         

            seedData(modelBuilder);

            base.OnModelCreating(modelBuilder);

        }

        public ModelBuilder seedData(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<MarryMonioUser>()
                .HasData(new MarryMonioUser { Id = new Guid("12345678-1234-1234-1234-123412341231"), Password= "$2b$10$5Lg7sMrtqv6Wrwmg5RczxezpgXmb5CCXn1q4bqBO1YwLar6kuC/6S", FirstName = "Johnny", LastName="Depp", Nickname="Sparrow", Email_Verified = true, Email = "1231@gmail.com", ProfilePicture= "https://res.cloudinary.com/dgegmm2pt/image/upload/v1717589458/monkey-face_kbdlvh.svg" });
            modelBuilder.Entity<MarryMonioUser>()
                .HasData(new MarryMonioUser { Id = new Guid("12345678-1234-1234-1234-123412341232"), Password = "$2b$10$Gadpk8GCgZE.dyzZFMHATOB0FT37Lt4DvBo4cO5PZ.0esE/7CrEK2", FirstName = "Amber", LastName="Heard", Nickname= "Amear", Email_Verified = true, Email = "1232@gmail.com" });

            modelBuilder.Entity<MarryMonioUser>()
                .HasData(new MarryMonioUser { Id = new Guid("12345678-1234-1234-1234-123412341233"), Password = "$2b$10$qeC9UbUklFhIZDpAKloBxO0pKerSvBdlZT.NoQHDgFwljAUWc35ra", FirstName = "Ola", LastName = "Nordmann", Nickname="Norsken", Email_Verified = true, Email = "1233@gmail.com" });
            modelBuilder.Entity<MarryMonioUser>()
                .HasData(new MarryMonioUser { Id = new Guid("12345678-1234-1234-1234-123412341234"), Password = "$2b$10$1hv7NchwECRiJ34DMUMPxuib2jfgADfqK6U1P.DNyLIMhMcirfC36", FirstName = "Kari", LastName = "Larsen", Nickname="Flau", Email_Verified = true, Email = "1234@gmail.com" });

            modelBuilder.Entity<MarryMonioUser>()
             .HasData(new MarryMonioUser { Id = new Guid("12345678-1234-1234-1234-123412341235"), Password = "$2b$10$XFQAF6s8DTFRmVfUXf4LDeZFtwS68TOfx6hgv8DfsvNCmuweCeC7.", FirstName = "Piler", LastName = "Heard", Nickname="Smear", Email_Verified = true, Email = "1235@gmail.com" });
            modelBuilder.Entity<MarryMonioUser>()
             .HasData(new MarryMonioUser { Id = new Guid("12345678-1234-1234-1234-123412341236"), Password = "$2b$10$ZOlVhh3DI.fQNrWg04Pli.cw7CcI9VAMSApGSR8l14uBNCN5KMvvq", FirstName = "Lise", LastName = "Larsen",Nickname="Stokk", Email_Verified = true, Email = "1236@gmail.com" });



            modelBuilder.Entity<Wedding>()
                .HasData(new Models.Wedding { Id = 1, Description = "We will be together for all time!", Dresscode = "FineClothing"});
            modelBuilder.Entity<Wedding>()
                .HasData(new Wedding { Id = 2, Description = "Samler hele familien for ett flott bryllup, vi skal være sammen for alltid", Dresscode = "Bunad" });

            modelBuilder.Entity<Participant>()
                .HasData(new Participant() { UserId = new Guid("12345678-1234-1234-1234-123412341231"), WeddingId = 1, Role = "Husband" });
            modelBuilder.Entity<Participant>()
                .HasData(new Participant() { UserId = new Guid("12345678-1234-1234-1234-123412341232"), WeddingId = 1, Role = "Wife" });

            modelBuilder.Entity<Participant>()
                .HasData(new Participant() { UserId = new Guid("12345678-1234-1234-1234-123412341233"), WeddingId = 2, Role = "Ektemann" });
            modelBuilder.Entity<Participant>()
                .HasData(new Participant() { UserId = new Guid("12345678-1234-1234-1234-123412341234"), WeddingId = 2, Role = "Kone" });

            modelBuilder.Entity<Participant>()
                .HasData(new Participant() { UserId = new Guid("12345678-1234-1234-1234-123412341235"), WeddingId = 1, Role = "Guest" });
            modelBuilder.Entity<Participant>()
                .HasData(new Participant() { UserId = new Guid("12345678-1234-1234-1234-123412341236"), WeddingId = 2, Role = "Gjest" });


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
                .HasData(new MenuOption { Id = 1, DishName = "Russian Kaviar", Alergens = Allergen.Fish, ReceptionId = 1, Tags = DishTags.Dinner+","+DishTags.Fish, Image = "https://res.cloudinary.com/dgegmm2pt/image/upload/v1717588973/caviar-1-svgrepo-com_kybu3j.svg" });
            modelBuilder.Entity<MenuOption>()
                .HasData(new MenuOption { Id = 2, DishName = "Sweltzer Salad", Alergens = Allergen.Lupin, ReceptionId = 1, Tags = DishTags.Dinner+","+DishTags.Vegan, Image = "https://res.cloudinary.com/dgegmm2pt/image/upload/v1717588973/salad-svgrepo-com_youfu3.svg" });
            modelBuilder.Entity<MenuOption>()
                .HasData(new MenuOption { Id = 3, DishName = "Ceasar Salad", Alergens = Allergen.Pistachio, ReceptionId = 2, Tags = DishTags.Dinner+","+DishTags.Vegan, Image = "https://res.cloudinary.com/dgegmm2pt/image/upload/v1717588973/salad-svgrepo-com_youfu3.svg" });
            modelBuilder.Entity<MenuOption>()
                .HasData(new MenuOption { Id = 4, DishName = "Tartare", Alergens = Allergen.None, ReceptionId = 2, Tags = DishTags.Dinner+","+DishTags.Meat, Image = "https://res.cloudinary.com/dgegmm2pt/image/upload/v1717588973/beef-svgrepo-com_seskgw.svg" });
            
            modelBuilder.Entity<MenuOption>()
                .HasData(new MenuOption { Id = 5, DishName = "Pizza", Alergens = Allergen.Lupin+","+Allergen.Wheat, ReceptionId = 1, Tags = DishTags.Dinner+","+DishTags.Meat, Image = "https://res.cloudinary.com/dgegmm2pt/image/upload/v1717669314/fi-rr-pizza-slice_shtxjk.svg" });
            modelBuilder.Entity<MenuOption>()
                .HasData(new MenuOption { Id = 6, DishName = "Croissant", Alergens = Allergen.Wheat, ReceptionId = 1, Tags = DishTags.Dessert+","+DishTags.Vegan, Image = "https://res.cloudinary.com/dgegmm2pt/image/upload/v1717669162/fi-rr-croissant_detk5k.svg" });
            modelBuilder.Entity<MenuOption>()
                .HasData(new MenuOption { Id = 7, DishName = "OreoCake", Alergens = Allergen.Wheat+","+Allergen.Milk, ReceptionId = 1, Tags = DishTags.Dessert+","+DishTags.Dairy, Image = "https://res.cloudinary.com/dgegmm2pt/image/upload/v1717669162/fi-rr-cake-birthday_ilnva6.svg" });
            modelBuilder.Entity<MenuOption>()
                .HasData(new MenuOption { Id = 8, DishName = "IceCream", Alergens = Allergen.Milk, ReceptionId = 1, Tags = DishTags.Dessert+","+DishTags.Dairy, Image = "https://res.cloudinary.com/dgegmm2pt/image/upload/fi-rr-ice-cream_xnkmus.svg" });
            modelBuilder.Entity<MenuOption>()
                .HasData(new MenuOption { Id = 9, DishName = "Cocktail", Alergens = Allergen.None, ReceptionId = 1, Tags = DishTags.Dessert, Image = "https://res.cloudinary.com/dgegmm2pt/image/upload/v1717669162/fi-rr-cocktail_ydvrfc.svg" });
            modelBuilder.Entity<MenuOption>()
                .HasData(new MenuOption { Id = 10, DishName = "Croissant", Alergens = Allergen.Wheat, ReceptionId = 1, Tags = DishTags.Dessert + "," + DishTags.Vegan, Image = "https://res.cloudinary.com/dgegmm2pt/image/upload/v1717669162/fi-rr-croissant_detk5k.svg" });



            modelBuilder.Entity<ReligiousCeremony>()
                .HasData(new ReligiousCeremony { Id = 1, LocationId = 1, WeddingId = 1, Date = DateTime.UtcNow.AddMinutes(1), Description = "LAS VEGAS Baby, come and meet ous for the church!" });
            modelBuilder.Entity<ReligiousCeremony>()
                .HasData(new ReligiousCeremony { Id = 2, LocationId = 3, WeddingId = 2, Date = DateTime.UtcNow.AddMinutes(12), Description = "Kom til vår sermoni der vi sier våre løfter" });


            modelBuilder.Entity<Post>()
                .HasData(new Post() { Id = 1, AuthorId = new Guid("12345678-1234-1234-1234-123412341231"), WeddingId = 1, Body = "Funky times, we will have loads of dance and party!!", Title = "PARTY!" });
            modelBuilder.Entity<Post>()
                .HasData(new Post() { Id = 2, AuthorId = new Guid("12345678-1234-1234-1234-123412341233"), WeddingId = 2, Body = "Hei, vi ønsker oss disse gavene: - Penger, Gavekort", Title = "Gaveliste" });

            modelBuilder.Entity<PostImage>()
                .HasData(new PostImage() { Id = 1, PostId = 1, URI = "https://no.wikipedia.org/wiki/Johnny_Depp#/media/Fil:Johnny_Depp_2020.jpg", Role = "MainImage", Size = "245kb" });
            modelBuilder.Entity<PostImage>()
                .HasData(new PostImage() { Id = 2, PostId = 1, URI = "https://upload.wikimedia.org/wikipedia/commons/thumb/e/e3/Johnny_Depp%27s_signature.svg/512px-Johnny_Depp%27s_signature.svg.png", Role = "SideImage", Size = "15kb" });
            modelBuilder.Entity<PostImage>()
                .HasData(new PostImage() { Id = 3, PostId = 2, URI = "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d8/Vinmonopolets_logo.jpg/600px-Vinmonopolets_logo.jpg", Role = "SideImage", Size = "15kb" });
            modelBuilder.Entity<PostImage>()
                .HasData(new PostImage() { Id = 4, PostId = 2, URI = "https://upload.wikimedia.org/wikipedia/commons/9/9d/Red_Christmas_present_on_white_background.jpg", Role = "MainImage", Size = "155kb" });

            modelBuilder.Entity<RSVP> ()
                .HasData(new RSVP() { Id = 1, WeddingId = 1, Body = "Come to my wedding, there will be free alcohol", SignerId = new Guid("12345678-1234-1234-1234-123412341235"), Deadline=DateTime.UtcNow.AddDays(14), NumberOfGuests=1, Status="Pending", OtherDietaryRequirements="None"});
            modelBuilder.Entity<RSVP>()
               .HasData(new RSVP() { Id = 2, WeddingId = 1, Body = "Come to my wedding, there will be free alcohol", SignerId = new Guid("12345678-1234-1234-1234-123412341231"), Deadline = DateTime.UtcNow.AddDays(14), NumberOfGuests = 1, Status = "Coming", OtherDietaryRequirements = "None" });
            modelBuilder.Entity<RSVP>()
               .HasData(new RSVP() { Id = 3, WeddingId = 1, Body = "Come to my wedding, there will be free alcohol", SignerId = new Guid("12345678-1234-1234-1234-123412341232"), Deadline = DateTime.UtcNow.AddDays(14), NumberOfGuests = 1, Status = "Coming", OtherDietaryRequirements = "None" });
            modelBuilder.Entity<RSVP>()
                .HasData(new RSVP() { Id = 4, WeddingId = 2, Body = "Kom til det fantastiske brylluppet mitt", SignerId = new Guid("12345678-1234-1234-1234-123412341236"), Deadline = DateTime.UtcNow.AddMonths(8), NumberOfGuests = 1, Status = "Avventer", OtherDietaryRequirements = "None" });
            modelBuilder.Entity<RSVP>()
                .HasData(new RSVP() { Id = 5, WeddingId = 2, Body = "Kom til det fantastiske brylluppet mitt", SignerId = new Guid("12345678-1234-1234-1234-123412341233"), Deadline = DateTime.UtcNow.AddMonths(8), NumberOfGuests = 1, Status = "Kommer", OtherDietaryRequirements = "None" });
            modelBuilder.Entity<RSVP>()
                .HasData(new RSVP() { Id = 6, WeddingId = 2, Body = "Kom til det fantastiske brylluppet mitt", SignerId = new Guid("12345678-1234-1234-1234-123412341234"), Deadline = DateTime.UtcNow.AddMonths(8), NumberOfGuests = 1, Status = "Kommer", OtherDietaryRequirements = "None" });

            return modelBuilder;
        }

    }
}
