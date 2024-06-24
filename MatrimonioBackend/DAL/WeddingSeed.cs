using MatrimonioBackend.Models.Constants;
using MatrimonioBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace MatrimonioBackend.DAL
{
    public static class WeddingSeed
    {


        public static ModelBuilder seedModelBuilder(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<MarryMonioUser>()
                .HasData(new MarryMonioUser { Id = new Guid("12345678-1234-1234-1234-123412341231"), Password = "$2b$10$5Lg7sMrtqv6Wrwmg5RczxezpgXmb5CCXn1q4bqBO1YwLar6kuC/6S", FirstName = "Johnny", LastName = "Depp", Nickname = "Sparrow", Email_Verified = true, Email = "1231@gmail.com", ProfilePicture = "https://res.cloudinary.com/dgegmm2pt/image/upload/v1717589458/monkey-face_kbdlvh.svg" });
            modelBuilder.Entity<MarryMonioUser>()
                .HasData(new MarryMonioUser { Id = new Guid("12345678-1234-1234-1234-123412341232"), Password = "$2b$10$Gadpk8GCgZE.dyzZFMHATOB0FT37Lt4DvBo4cO5PZ.0esE/7CrEK2", FirstName = "Amber", LastName = "Heard", Nickname = "Amear", Email_Verified = true, Email = "1232@gmail.com" });

            modelBuilder.Entity<MarryMonioUser>()
                .HasData(new MarryMonioUser { Id = new Guid("12345678-1234-1234-1234-123412341233"), Password = "$2b$10$qeC9UbUklFhIZDpAKloBxO0pKerSvBdlZT.NoQHDgFwljAUWc35ra", FirstName = "Ola", LastName = "Nordmann", Nickname = "Norsken", Email_Verified = true, Email = "1233@gmail.com" });
            modelBuilder.Entity<MarryMonioUser>()
                .HasData(new MarryMonioUser { Id = new Guid("12345678-1234-1234-1234-123412341234"), Password = "$2b$10$1hv7NchwECRiJ34DMUMPxuib2jfgADfqK6U1P.DNyLIMhMcirfC36", FirstName = "Kari", LastName = "Larsen", Nickname = "Flau", Email_Verified = true, Email = "1234@gmail.com" });

            modelBuilder.Entity<MarryMonioUser>()
             .HasData(new MarryMonioUser { Id = new Guid("12345678-1234-1234-1234-123412341235"), Password = "$2b$10$XFQAF6s8DTFRmVfUXf4LDeZFtwS68TOfx6hgv8DfsvNCmuweCeC7.", FirstName = "Piler", LastName = "Heard", Nickname = "Smear", Email_Verified = true, Email = "1235@gmail.com" });
            modelBuilder.Entity<MarryMonioUser>()
             .HasData(new MarryMonioUser { Id = new Guid("12345678-1234-1234-1234-123412341236"), Password = "$2b$10$ZOlVhh3DI.fQNrWg04Pli.cw7CcI9VAMSApGSR8l14uBNCN5KMvvq", FirstName = "Lise", LastName = "Larsen", Nickname = "Stokk", Email_Verified = true, Email = "1236@gmail.com" });



            modelBuilder.Entity<Wedding>()
                .HasData(new Models.Wedding
                {
                    id = 1,
                    description = "We will be together for all time!",
                    dresscode = "FineClothing",
                    backgroundImage = "",
                    bodyFont = "Helvetica, Inter, system-ui, Avenir, Arial, sans-serif, Tangerine",
                    headingFont = "Tangerine, Helvetica, Inter, system-ui, Avenir, Arial, sans-serif",
                    primaryColor = "#24d07e54",
                    secoundaryColor = "#4b33c1ff",
                    primaryFontColor = "#cca548",
                    secoundaryFontColor = "#1cc642",
                    picture = "https://res.cloudinary.com/dgegmm2pt/image/upload/v1719219745/weddings-2015-02-johnny-depp-amber-heard-wedding-pictures-celebrity-weddings-0202-getty-main_spytsr.webp",
                    title= "J + A, Hasta la muerte",

                });
            modelBuilder.Entity<Wedding>()
                .HasData(new Wedding
                {
                    id = 2,
                    description = "Samler hele familien for ett flott bryllup, vi skal være sammen for alltid",
                    dresscode = "Bunad",
                    backgroundImage = "https://res.cloudinary.com/dgegmm2pt/image/upload/v1719223998/Adolph_Tidemand___Hans_Gude_-_Bridal_Procession_on_the_Hardangerfjord_-_Google_Art_Project_nahsns.jp",
                    bodyFont = "Helvetica, Inter, system-ui, Avenir, Arial, sans-serif, Tangerine",
                    headingFont = "Arial, Tangerine, Helvetica, Inter, system-ui, Avenir, sans-serif",
                    primaryColor = "#3975ce97",
                    secoundaryColor = "#7837f097",
                    primaryFontColor = "#fed3e3",
                    secoundaryFontColor = "#d84444",
                    picture= "https://res.cloudinary.com/dgegmm2pt/image/upload/v1719219742/ce776ba7c55c1597a6cd57cbf5684ecaNorway_edmtmp.jpg",
                    title="Brudeferd i Hardanger"
                });

            modelBuilder.Entity<Participant>()
                .HasData(new Participant() { UserId = new Guid("12345678-1234-1234-1234-123412341231"), WeddingId = 1, Role = "Husband" });
            modelBuilder.Entity<Participant>()
                .HasData(new Participant() { UserId = new Guid("12345678-1234-1234-1234-123412341232"), WeddingId = 1, Role = "Wife" });

            modelBuilder.Entity<Participant>()
                .HasData(new Participant() { UserId = new Guid("12345678-1234-1234-1234-123412341233"), WeddingId = 2, Role = "Husband" });
            modelBuilder.Entity<Participant>()
                .HasData(new Participant() { UserId = new Guid("12345678-1234-1234-1234-123412341234"), WeddingId = 2, Role = "Wife" });

            modelBuilder.Entity<Participant>()
                .HasData(new Participant() { UserId = new Guid("12345678-1234-1234-1234-123412341235"), WeddingId = 1, Role = "Guest" });
            modelBuilder.Entity<Participant>()
                .HasData(new Participant() { UserId = new Guid("12345678-1234-1234-1234-123412341236"), WeddingId = 2, Role = "Guest" });
            modelBuilder.Entity<Participant>()
                .HasData(new Participant() { UserId = new Guid("12345678-1234-1234-1234-123412341231"), WeddingId = 2, Role = "Guest" });

           // 8100 W Robindale Rd, Las Vegas, NV 89113, USA
            modelBuilder.Entity<Location>()
                .HasData(new Models.Location { Id = 1, Body = "Beautiful church inside a golf court", Title = "Amazing Church", Lat = 37.4, Lng = -115.1,
                    Address= "8100 W Robindale Rd", Country = "USA", Region="NV 89113", Placename = "Las Vegas", Image= "https://res.cloudinary.com/dgegmm2pt/image/upload/v1718967182/imagesLA_p8dwav.jpg"
                });
            //E 72nd St, New York, NY 10023, USA
            modelBuilder.Entity<Location>()
                .HasData(new Models.Location { Id = 2, Body = "Open-Air dancefield suitable for any occation", Title = "Central Park", Lat = 40.77, Lng = -73.97,
                                Address= "E 72nd St", Country = "USA", Region= "NY 10023", Placename = "New York", Image= "https://res.cloudinary.com/dgegmm2pt/image/upload/v1718967182/imagesNY_owtaqw.jpg"
                });
            //Ressveien 13, 7336 Meldal
            modelBuilder.Entity<Location>()
                .HasData(new Models.Location { Id = 3, Body = "Tradisjonsrik kirke, gjennoppreist etter samme design som middelalder kirken som brante ned i 1980-tallet", Title = "Nedre Bergslia Kirke", Lat = 63.2, Lng = 9.85,
                    Address = "Ressveien 13",
                    Country = "Norway",
                    Region = "Trondelag",
                    Placename = "7336 Meldal",
                    Image= "https://res.cloudinary.com/dgegmm2pt/image/upload/images_iakrbp.jpg"
                });
            //Karl Johans gt. 31, 0159 Oslo
            modelBuilder.Entity<Location>()
                .HasData(new Models.Location { Id = 4, Body = "Store lokaler vel egnet for enhver avdeling", Title = "Grand Plaza Hotel", Lat = 59.92, Lng = 10.78,
                    Address = "Karl Johans gt. 31",
                    Country = "Norway",
                    Region = "Oslo",
                    Placename = "0159 Oslo",
                    Image= "https://res.cloudinary.com/dgegmm2pt/image/upload/v1718967187/Grand_Hotel_Exterior_Facade_Summer_June_3rd_2020_K_mq9d0j.avif"
                });

            modelBuilder.Entity<Reception>()
                .HasData(new Models.Reception
                {
                    Id = 1,
                    StartDate = DateTime.UtcNow.AddMonths(1).AddHours(2),
                    EndDate = DateTime.UtcNow.AddMonths(1).AddHours(6),
                    Description = "ONLY VIP, You need atleast 1million IMDB hits",
                    LocationId = 2,
                    WeddingId = 1
                });
            modelBuilder.Entity<Reception>()
                .HasData(new Models.Reception
                {
                    Id = 2,
                    StartDate = DateTime.UtcNow.AddMonths(12).AddHours(3),
                    EndDate = DateTime.UtcNow.AddMonths(12).AddHours(6),
                    Description = "A professional and comfortable experience with great food",
                    LocationId = 4,
                    WeddingId = 2
                });

            modelBuilder.Entity<MenuOption>()
                .HasData(new MenuOption { Id = 1, DishName = "Russian Kaviar", Alergens = Allergen.Fish, ReceptionId = 1, Tags = DishTags.Dinner + "," + DishTags.Fish, Image = "https://res.cloudinary.com/dgegmm2pt/image/upload/v1717588973/caviar-1-svgrepo-com_kybu3j.svg" });
            modelBuilder.Entity<MenuOption>()
                .HasData(new MenuOption { Id = 2, DishName = "Sweltzer Salad", Alergens = Allergen.Lupin, ReceptionId = 1, Tags = DishTags.Dinner + "," + DishTags.Vegan, Image = "https://res.cloudinary.com/dgegmm2pt/image/upload/v1717588973/salad-svgrepo-com_youfu3.svg" });
            modelBuilder.Entity<MenuOption>()
                .HasData(new MenuOption { Id = 3, DishName = "Ceasar Salad", Alergens = Allergen.Pistachio, ReceptionId = 2, Tags = DishTags.Dinner + "," + DishTags.Vegan, Image = "https://res.cloudinary.com/dgegmm2pt/image/upload/v1717588973/salad-svgrepo-com_youfu3.svg" });
            modelBuilder.Entity<MenuOption>()
                .HasData(new MenuOption { Id = 4, DishName = "Tartare", Alergens = Allergen.None, ReceptionId = 2, Tags = DishTags.Dinner + "," + DishTags.Meat, Image = "https://res.cloudinary.com/dgegmm2pt/image/upload/v1717588973/beef-svgrepo-com_seskgw.svg" });

            modelBuilder.Entity<MenuOption>()
                .HasData(new MenuOption { Id = 5, DishName = "Pizza", Alergens = Allergen.Lupin + "," + Allergen.Wheat, ReceptionId = 1, Tags = DishTags.Dinner + "," + DishTags.Meat, Image = "https://res.cloudinary.com/dgegmm2pt/image/upload/v1717669314/fi-rr-pizza-slice_shtxjk.svg" });
            modelBuilder.Entity<MenuOption>()
                .HasData(new MenuOption { Id = 6, DishName = "Croissant", Alergens = Allergen.Wheat, ReceptionId = 1, Tags = DishTags.Dessert + "," + DishTags.Vegan, Image = "https://res.cloudinary.com/dgegmm2pt/image/upload/v1717669162/fi-rr-croissant_detk5k.svg" });
            modelBuilder.Entity<MenuOption>()
                .HasData(new MenuOption { Id = 7, DishName = "OreoCake", Alergens = Allergen.Wheat + "," + Allergen.Milk, ReceptionId = 1, Tags = DishTags.Dessert + "," + DishTags.Dairy, Image = "https://res.cloudinary.com/dgegmm2pt/image/upload/v1717669162/fi-rr-cake-birthday_ilnva6.svg" });
            modelBuilder.Entity<MenuOption>()
                .HasData(new MenuOption { Id = 8, DishName = "IceCream", Alergens = Allergen.Milk, ReceptionId = 1, Tags = DishTags.Dessert + "," + DishTags.Dairy, Image = "https://res.cloudinary.com/dgegmm2pt/image/upload/fi-rr-ice-cream_xnkmus.svg" });
            modelBuilder.Entity<MenuOption>()
                .HasData(new MenuOption { Id = 9, DishName = "Cocktail", Alergens = Allergen.None, ReceptionId = 2, Tags = DishTags.Dessert, Image = "https://res.cloudinary.com/dgegmm2pt/image/upload/v1717669162/fi-rr-cocktail_ydvrfc.svg" });
            modelBuilder.Entity<MenuOption>()
                .HasData(new MenuOption { Id = 10, DishName = "Croissant", Alergens = Allergen.Wheat, ReceptionId = 2, Tags = DishTags.Dessert + "," + DishTags.Vegan, Image = "https://res.cloudinary.com/dgegmm2pt/image/upload/v1717669162/fi-rr-croissant_detk5k.svg" });



            modelBuilder.Entity<ReligiousCeremony>()
                .HasData(new ReligiousCeremony { Id = 1, LocationId = 1, WeddingId = 1, 
                    StartDate = DateTime.UtcNow.AddMonths(1), 
                    EndDate = DateTime.UtcNow.AddMonths(1).AddHours(1), 
                    Description = "LAS VEGAS Baby, come and meet ous for the church!" });
            modelBuilder.Entity<ReligiousCeremony>()
                .HasData(new ReligiousCeremony { Id = 2, LocationId = 3, WeddingId = 2, 
                    StartDate = DateTime.UtcNow.AddMonths(12),
                    EndDate = DateTime.UtcNow.AddMonths(12).AddHours(2), 
                    Description = "Kom til vår sermoni der vi sier våre løfter" });


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

            modelBuilder.Entity<RSVP>()
                .HasData(new RSVP() { Id = 1, WeddingId = 1, Body = "Come to my wedding, there will be free alcohol", SignerId = new Guid("12345678-1234-1234-1234-123412341235"), Deadline = DateTime.UtcNow.AddDays(14), NumberOfGuests = 1, Status = RSVPStatus.Accepted, OtherDietaryRequirements = "None" });
            modelBuilder.Entity<RSVP>()
               .HasData(new RSVP() { Id = 2, WeddingId = 1, Body = "Come to my wedding, there will be free alcohol", SignerId = new Guid("12345678-1234-1234-1234-123412341231"), Deadline = DateTime.UtcNow.AddDays(14), NumberOfGuests = 1, Status = RSVPStatus.Accepted, OtherDietaryRequirements = "None" });
            modelBuilder.Entity<RSVP>()
               .HasData(new RSVP() { Id = 3, WeddingId = 1, Body = "Come to my wedding, there will be free alcohol", SignerId = new Guid("12345678-1234-1234-1234-123412341232"), Deadline = DateTime.UtcNow.AddDays(14), NumberOfGuests = 1, Status = RSVPStatus.Accepted, OtherDietaryRequirements = "None" });
            modelBuilder.Entity<RSVP>()
                .HasData(new RSVP() { Id = 4, WeddingId = 2, Body = "Kom til det fantastiske brylluppet mitt", SignerId = new Guid("12345678-1234-1234-1234-123412341236"), Deadline = DateTime.UtcNow.AddMonths(8), NumberOfGuests = 1, Status = RSVPStatus.Pending, OtherDietaryRequirements = "None" });
            modelBuilder.Entity<RSVP>()
                .HasData(new RSVP() { Id = 5, WeddingId = 2, Body = "Kom til det fantastiske brylluppet mitt", SignerId = new Guid("12345678-1234-1234-1234-123412341233"), Deadline = DateTime.UtcNow.AddMonths(8), NumberOfGuests = 1, Status = RSVPStatus.Accepted, OtherDietaryRequirements = "None" });
            modelBuilder.Entity<RSVP>()
                .HasData(new RSVP() { Id = 6, WeddingId = 2, Body = "Kom til det fantastiske brylluppet mitt", SignerId = new Guid("12345678-1234-1234-1234-123412341234"), Deadline = DateTime.UtcNow.AddMonths(8), NumberOfGuests = 1, Status = RSVPStatus.Accepted, OtherDietaryRequirements = "None" });

            return modelBuilder;
        }
    }
}
