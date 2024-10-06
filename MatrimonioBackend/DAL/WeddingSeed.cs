using MatrimonioBackend.Models.Constants;
using MatrimonioBackend.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace MatrimonioBackend.DAL
{
    public static class WeddingSeed
    {


        public static ModelBuilder seedModelBuilder(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<MarryMonioUser>()
                .HasData(new MarryMonioUser { Id = new Guid("12345678-1234-1234-1234-123412341231"), Password = "$2b$10$5Lg7sMrtqv6Wrwmg5RczxezpgXmb5CCXn1q4bqBO1YwLar6kuC/6S", FirstName = "Johnny", LastName = "Depp", Nickname = "Sparrow", Email_Verified = true, Email = "1231@gmail.com", ProfilePicture = "https://res.cloudinary.com/dgegmm2pt/image/upload/v1717589458/monkey-face_kbdlvh.svg", Language = Language.IT });
            modelBuilder.Entity<MarryMonioUser>()
                .HasData(new MarryMonioUser { Id = new Guid("12345678-1234-1234-1234-123412341232"), Password = "$2b$10$Gadpk8GCgZE.dyzZFMHATOB0FT37Lt4DvBo4cO5PZ.0esE/7CrEK2", FirstName = "Amber", LastName = "Heard", Nickname = "Amear", Email_Verified = true, Email = "1232@gmail.com", Language= Language.EN });

            modelBuilder.Entity<MarryMonioUser>()
                .HasData(new MarryMonioUser { Id = new Guid("12345678-1234-1234-1234-123412341233"), Password = "$2b$10$qeC9UbUklFhIZDpAKloBxO0pKerSvBdlZT.NoQHDgFwljAUWc35ra", FirstName = "Ola", LastName = "Nordmann", Nickname = "Norsken", Email_Verified = true, Email = "1233@gmail.com", Language = Language.NO });
            modelBuilder.Entity<MarryMonioUser>()
                .HasData(new MarryMonioUser { Id = new Guid("12345678-1234-1234-1234-123412341234"), Password = "$2b$10$1hv7NchwECRiJ34DMUMPxuib2jfgADfqK6U1P.DNyLIMhMcirfC36", FirstName = "Kari", LastName = "Larsen", Nickname = "Flau", Email_Verified = true, Email = "1234@gmail.com", Language = Language.NO });

            modelBuilder.Entity<MarryMonioUser>()
             .HasData(new MarryMonioUser { Id = new Guid("12345678-1234-1234-1234-123412341235"), Password = "$2b$10$XFQAF6s8DTFRmVfUXf4LDeZFtwS68TOfx6hgv8DfsvNCmuweCeC7.", FirstName = "Piler", LastName = "Heard", Nickname = "Smear", Email_Verified = true, Email = "1235@gmail.com" , Language = Language.IT });
            modelBuilder.Entity<MarryMonioUser>()
             .HasData(new MarryMonioUser { Id = new Guid("12345678-1234-1234-1234-123412341236"), Password = "$2b$10$ZOlVhh3DI.fQNrWg04Pli.cw7CcI9VAMSApGSR8l14uBNCN5KMvvq", FirstName = "Lise", LastName = "Larsen", Nickname = "Stokk", Email_Verified = true, Email = "1236@gmail.com" , Language = Language.NO });



            modelBuilder.Entity<Wedding>()
                .HasData(new Models.Wedding
                {
                    id = 1,
                    backgroundImage = "",
                    bodyFont = "Helvetica, Inter, system-ui, Avenir, Arial, sans-serif, Tangerine",
                    headingFont = "Tangerine, Helvetica, Inter, system-ui, Avenir, Arial, sans-serif",
                    primaryColor = "#24d07e54",
                    secoundaryColor = "#4b33c1ff",
                    primaryFontColor = "#cca548",
                    secoundaryFontColor = "#1cc642",
                    picture = "https://res.cloudinary.com/dgegmm2pt/image/upload/v1719219745/weddings-2015-02-johnny-depp-amber-heard-wedding-pictures-celebrity-weddings-0202-getty-main_spytsr.webp",
                    
                });
            modelBuilder.Entity<WeddingTranslation>()
                .HasData(new Models.WeddingTranslation
                {
                    Description = "We will be together for all time!",
                    Dresscode = "FineClothing",
                    IsDefaultLanguage = true,
                    Language = "EN",
                    Title = "J + A, Hasta la muerte",
                    WeddingId = 1,
                    RSVPBody = "Wanna join my wedding?"

                });
            modelBuilder.Entity<WeddingTranslation>()
            .HasData(new Models.WeddingTranslation
            {
                Description = "Nostra insieme per alle ora!",
                Dresscode = "Bene Vestiti",
                IsDefaultLanguage = false,
                Language = "IT",
                Title = "J + A, arrividerci morta",
                WeddingId = 1,
                RSVPBody = "Vieni alla nostra festa?"
            });
            modelBuilder.Entity<Wedding>()
                .HasData(new Wedding
                {
                    id = 2,
                    backgroundImage = "https://res.cloudinary.com/dgegmm2pt/image/upload/v1719223998/Adolph_Tidemand___Hans_Gude_-_Bridal_Procession_on_the_Hardangerfjord_-_Google_Art_Project_nahsns.jpg",
                    bodyFont = "Helvetica, Inter, system-ui, Avenir, Arial, sans-serif, Tangerine",
                    headingFont = "Arial, Tangerine, Helvetica, Inter, system-ui, Avenir, sans-serif",
                    primaryColor = "#3975ce97",
                    secoundaryColor = "#7837f097",
                    primaryFontColor = "#fed3e3",
                    secoundaryFontColor = "#d84444",
                    picture = "https://res.cloudinary.com/dgegmm2pt/image/upload/v1719219742/ce776ba7c55c1597a6cd57cbf5684ecaNorway_edmtmp.jpg",
                    
                });
            modelBuilder.Entity<WeddingTranslation>()
                .HasData(new Models.WeddingTranslation
                {
                    Description = "Samler hele familien for ett flott bryllup, vi skal være sammen for alltid",
                    Dresscode = "Bunad",
                    IsDefaultLanguage = true,
                    Language = "EN",
                    Title = "Brudeferd i Hardanger",
                    WeddingId = 2,
                    RSVPBody = "Vil du kommer på bryllup?"
                });
            modelBuilder.Entity<Wedding>()
                .HasData(new Models.Wedding
                {
                    id = 3,
                    backgroundImage = "https://res.cloudinary.com/dgegmm2pt/image/upload/v1719657106/Alternative-Engagement-rings-untraditional-nontraditional-wedding-photographer-2-of-7-1024x683_fsqqvk.jpg",
                    bodyFont = "Helvetica, Inter, system-ui, Avenir, Arial, sans-serif, Tangerine",
                    headingFont = "Tangerine, Helvetica, Inter, system-ui, Avenir, Arial, sans-serif",
                    primaryColor = "#00572c79",
                    secoundaryColor = "#ffffffea",
                    primaryFontColor = "#fed3e3",
                    secoundaryFontColor = "#000000",
                    picture = "https://res.cloudinary.com/dgegmm2pt/image/upload/v1719657077/WhatsApp_Image_2024-03-03_at_09.00.54_b621970c_fslkkq.jpg",
                });
            modelBuilder.Entity<WeddingTranslation>()
            .HasData(new Models.WeddingTranslation
            {
                Description = "Benevenuti al nostro matrimonio",
                Dresscode = "Bei vestiti",
                IsDefaultLanguage = true,
                Language = Language.IT,
                Title = "Amore",
                WeddingId = 3,
                RSVPBody = "Vieni alla nostra festa?"
            });



            var husband1 = new Participant() { UserId = new Guid("12345678-1234-1234-1234-123412341231"), WeddingId = 1 };
            var wife1 = new Participant() { UserId = new Guid("12345678-1234-1234-1234-123412341232"), WeddingId = 1 };
            modelBuilder.Entity<Participant>()
                .HasData(husband1);
            modelBuilder.Entity<Participant>()
                .HasData(wife1);

            modelBuilder.Entity<ParticipantTranslation>()
                .HasData(new ParticipantTranslation()
                {
                    Language = "EN",
                    IsDefaultLanguage = true,
                    Role = "Husband",
                    WeddingId = husband1.WeddingId,
                    UserId = husband1.UserId
                });
            modelBuilder.Entity<ParticipantTranslation>()
                .HasData(new ParticipantTranslation()
                {
                    Language = "EN",
                    IsDefaultLanguage = true,
                    Role = "Wife",
                    WeddingId = wife1.WeddingId,
                    UserId = wife1.UserId
                });

            var husband2 = new Participant() { UserId = new Guid("12345678-1234-1234-1234-123412341233"), WeddingId = 2 };
            var wife2 = new Participant() { UserId = new Guid("12345678-1234-1234-1234-123412341234"), WeddingId = 2 };
            modelBuilder.Entity<Participant>()
                .HasData(husband2);
            modelBuilder.Entity<Participant>()
                .HasData(wife2);

            modelBuilder.Entity<ParticipantTranslation>()
                .HasData(new ParticipantTranslation()
                {
                    Language = "EN",
                    IsDefaultLanguage = true,
                    Role = "Husband",
                    WeddingId = husband2.WeddingId,
                    UserId = husband2.UserId
                });
            modelBuilder.Entity<ParticipantTranslation>()
                .HasData(new ParticipantTranslation()
                {
                    Language = "EN",
                    IsDefaultLanguage = true,
                    Role = "Wife",
                    WeddingId = wife2.WeddingId,
                    UserId = wife2.UserId
                });

            var guest1 = new Participant()
            { UserId = new Guid("12345678-1234-1234-1234-123412341235"), WeddingId = 1 };
            var guest2 = new Participant()
            { UserId = new Guid("12345678-1234-1234-1234-123412341236"), WeddingId = 2 };
            var guest3 = new Participant()
            { UserId = new Guid("12345678-1234-1234-1234-123412341231"), WeddingId = 2 };
            var guest4 = new Participant()
            {
                UserId = new Guid("12345678-1234-1234-1234-123412341231"),
                WeddingId = 3
            };
            modelBuilder.Entity<Participant>()
                .HasData(guest1);
            modelBuilder.Entity<Participant>()
                .HasData(guest2);
            modelBuilder.Entity<Participant>()
                .HasData(guest3);
            modelBuilder.Entity<Participant>()
                .HasData(guest4);
            modelBuilder.Entity<ParticipantTranslation>()
                .HasData(new ParticipantTranslation()
                {
                    Language = "EN",
                    IsDefaultLanguage = true,
                    Role = "Guest",
                    WeddingId = guest1.WeddingId,
                    UserId = guest1.UserId
                });
            modelBuilder.Entity<ParticipantTranslation>()
                .HasData(new ParticipantTranslation()
                {
                    Language = "EN",
                    IsDefaultLanguage = true,
                    Role = "Guest",
                    WeddingId = guest2.WeddingId,
                    UserId = guest2.UserId
                });
            modelBuilder.Entity<ParticipantTranslation>()
                .HasData(new ParticipantTranslation()
                {
                    Language = "EN",
                    IsDefaultLanguage = true,
                    Role = "Guest",
                    WeddingId = guest3.WeddingId,
                    UserId = guest3.UserId
                });
            modelBuilder.Entity<ParticipantTranslation>()
                .HasData(new ParticipantTranslation()
                {
                    Language = "IT",
                    IsDefaultLanguage = true,
                    Role = "Ospite",
                    WeddingId = guest4.WeddingId,
                    UserId = guest4.UserId
                });



            // 8100 W Robindale Rd, Las Vegas, NV 89113, USA
            modelBuilder.Entity<Location>()
                .HasData(new Models.Location
                {
                    Id = 1,
                    Image = "https://res.cloudinary.com/dgegmm2pt/image/upload/v1718967182/imagesLA_p8dwav.jpg"
                });
            //E 72nd St, New York, NY 10023, USA
            modelBuilder.Entity<Location>()
                .HasData(new Models.Location
                {
                    Id = 2,
                    Image = "https://res.cloudinary.com/dgegmm2pt/image/upload/v1718967182/imagesNY_owtaqw.jpg"
                });
            //Ressveien 13, 7336 Meldal
            modelBuilder.Entity<Location>()
                .HasData(new Models.Location
                {
                    Id = 3,
                    Image = "https://res.cloudinary.com/dgegmm2pt/image/upload/images_iakrbp.jpg"
                });
            //Karl Johans gt. 31, 0159 Oslo
            modelBuilder.Entity<Location>()
                .HasData(new Models.Location
                {
                    Id = 4,
                    Image = "https://res.cloudinary.com/dgegmm2pt/image/upload/v1718967187/Grand_Hotel_Exterior_Facade_Summer_June_3rd_2020_K_mq9d0j.avif"

                });
            modelBuilder.Entity<Location>()
                .HasData(new Models.Location
                {
                    Id = 5,
                    Image = "https://res.cloudinary.com/dgegmm2pt/image/upload/v1719565137/sala_ric1_w42ijp.jpg"
                });
            modelBuilder.Entity<Location>()
                .HasData(new Location
                {
                    Id = 6,
                    Image = "https://res.cloudinary.com/dgegmm2pt/image/upload/v1719572219/93995948_113725873633376_7303443439893348352_n_c6fqtd.jpg"
                });

            modelBuilder.Entity<LocationTranslation>()
             .HasData(new LocationTranslation()
             {
                 Language = Language.IT,
                 IsDefaultLanguage = true, 
                 Body = "Sull’incantevole scenario dello stretto di Messina, al confine tra mare e cielo, dove il tramonto svanisce all’orizzonte, nasce il Ristorante Panorama. Elegante e raffinato, con una cucina dai sapori mediterranei, gode di una meravigliosa vista che racchiude i magnifici paesaggi siciliani che, collegati dal mare, diventano un tutt’uno con la splendida e suggestiva costa calabra. Dal 1995, il Panorama è diventato punto di riferimento gastronomico, grazie alla competenza, cortesia ed esperienza dei titolari, conosciuto per la cura dedicata all’ospitalità e per il servizio impeccabile, curato nei minimi dettagli da tutto lo staff di cucina e di sala.",
                 Title = "Panorama",
                 Address = "Panorama Via Enrico Cosenz, 89018 Villa San Giovanni RC, Italia",
                 Country = "Italia",
                 LocationId = 5,
             });

            modelBuilder.Entity<LocationTranslation>()
                .HasData(new LocationTranslation()
                {
                    IsDefaultLanguage = false,
                    Language = Language.EN,
                    Body = "Has a great panoramic view and specializes in seafood",
                    Title = "Panorama",
                    Address = "Panorama Via Enrico Cosenz, 89018 Villa San Giovanni RC, Italia",
                    Country = "Italy",
                    LocationId = 5,
                });

            modelBuilder.Entity<LocationTranslation>()
              .HasData(new LocationTranslation()
              {
                  IsDefaultLanguage = true,
                  Language = Language.IT,
                  Body = "Parrocchia di San Biagio Vescovo e Martire",
                  Title = "Chiesa di San Biagio",
                  Address = "Chiesa di San Biagio Via Trapani Lombardo, 29, 89135 Reggio Calabria RC, Italia",
                  Country = "Italia",
                  LocationId = 6,
              });

                    modelBuilder.Entity<LocationTranslation>()
          .HasData(new LocationTranslation()
          {
              IsDefaultLanguage = false,
              Language = Language.EN,
              Body = "Parish of Saint Blaise",
              Title = "Church of Saint Blaise",
              Address = "Chiesa di San Biagio Via Trapani Lombardo, 29, 89135 Reggio Calabria RC, Italia",
              Country = "Italy",
              LocationId = 6,
          });

            modelBuilder.Entity<LocationTranslation>()
              .HasData(new LocationTranslation()
              {
                  IsDefaultLanguage = true,
                  Language = "EN",
                  Body = "Tradisjonsrik kirke, gjennoppreist etter samme design som middelalder kirken som brante ned i 1980-tallet",
                  Title = "Nedre Bergslia Kirke",
                  Address = "Ressveien 13",
                  Country = "Norway",
                  LocationId = 3,
              });

            modelBuilder.Entity<LocationTranslation>()
              .HasData(new LocationTranslation()
              {
                  Language = "EN",
                  IsDefaultLanguage = true,
                  Body = "Beautiful church inside a golf court",
                  Title = "Amazing Church",
                  Address = "8100 W Robindale Rd",
                  Country = "USA",
                  LocationId = 2,
              });

            modelBuilder.Entity<LocationTranslation>()
              .HasData(new LocationTranslation()
              {
                  Language = "EN",
                  IsDefaultLanguage = true,
                  Body = "Open-Air dancefield suitable for any occation",
                  Title = "Central Park",
                  Address = "E 72nd St",
                  Country = "USA",
                  LocationId = 1,
              });
            modelBuilder.Entity<Reception>()
                .HasData(new Models.Reception
                {
                    Id = 1,
                    StartDate = new DateTime(2025, 1, 1, 10, 0, 0, DateTimeKind.Utc),
                    EndDate = new DateTime(2025, 2, 1, 12, 45, 0, DateTimeKind.Utc),
                    LocationId = 2,
                    WeddingId = 1
                });
            modelBuilder.Entity<Reception>()
                .HasData(new Models.Reception
                {
                    Id = 2,
                    StartDate = new DateTime(2025, 6, 1, 10, 0, 0, DateTimeKind.Utc),
                    EndDate = new DateTime(2025, 6, 2, 12, 30, 0, DateTimeKind.Utc),
                    LocationId = 4,
                    WeddingId = 2
                });
            modelBuilder.Entity<Reception>()
                .HasData(new Models.Reception
                {
                    Id = 3,
                    StartDate = new DateTime(2025, 6, 1, 10, 0, 0, DateTimeKind.Utc),
                    EndDate = new DateTime(2025, 6, 2, 12, 30, 0, DateTimeKind.Utc),
                    LocationId = 5,
                    WeddingId = 3
                });

            modelBuilder.Entity<ReceptionTranslation>()
                .HasData(new ReceptionTranslation()
                {
                    ReceptionId = 3,
                    Description = "A professional and comfortable experience with great food",
                    IsDefaultLanguage = true,
                    Language = Language.IT
                });

            modelBuilder.Entity<ReceptionTranslation>()
                .HasData(new ReceptionTranslation()
                {
                    ReceptionId = 2,
                    Description = "Accoglienza molto bella",
                    IsDefaultLanguage = true,
                    Language = Language.EN
                });

            modelBuilder.Entity<ReceptionTranslation>()
                .HasData(new ReceptionTranslation()
                {
                    ReceptionId = 1,
                    Description = "ONLY VIP, You need atleast 1million IMDB hits",
                    IsDefaultLanguage = true,
                    Language = Language.EN
                });

            modelBuilder.Entity<MenuOption>()
                .HasData(new MenuOption { Id = 1, ReceptionId = 1, Image = "https://res.cloudinary.com/dgegmm2pt/image/upload/v1721071653/child-svgrepo-com_ynxnpz.svg" });
            modelBuilder.Entity<MenuOption>()
                .HasData(new MenuOption { Id = 2, ReceptionId = 1, Image = "https://res.cloudinary.com/dgegmm2pt/image/upload/v1721071744/person-svgrepo-com_zyo42q.svg" });
            modelBuilder.Entity<MenuOption>()
                .HasData(new MenuOption { Id = 3, ReceptionId = 1, Image = "https://res.cloudinary.com/dgegmm2pt/image/upload/v1719569979/tomato-svgrepo-com_rtalrx.svg" });

            modelBuilder.Entity<MenuOption>()
                .HasData(new MenuOption { Id = 4, ReceptionId = 2, Image = "https://res.cloudinary.com/dgegmm2pt/image/upload/v1721071653/child-svgrepo-com_ynxnpz.svg" });
            modelBuilder.Entity<MenuOption>()
                .HasData(new MenuOption { Id = 5, ReceptionId = 2, Image = "https://res.cloudinary.com/dgegmm2pt/image/upload/v1721071744/person-svgrepo-com_zyo42q.svg" });
            modelBuilder.Entity<MenuOption>()
                .HasData(new MenuOption { Id = 6, ReceptionId = 2, Image = "https://res.cloudinary.com/dgegmm2pt/image/upload/v1719569979/tomato-svgrepo-com_rtalrx.svg" });

            modelBuilder.Entity<MenuOption>()
                .HasData(new MenuOption { Id = 7, ReceptionId = 3, Image = "https://res.cloudinary.com/dgegmm2pt/image/upload/v1721071653/child-svgrepo-com_ynxnpz.svg" });
            modelBuilder.Entity<MenuOption>()
                .HasData(new MenuOption { Id = 8, ReceptionId = 3, Image = "https://res.cloudinary.com/dgegmm2pt/image/upload/v1721071744/person-svgrepo-com_zyo42q.svg" });
            modelBuilder.Entity<MenuOption>()
                .HasData(new MenuOption { Id = 9, ReceptionId = 3, Image = "https://res.cloudinary.com/dgegmm2pt/image/upload/v1719569979/tomato-svgrepo-com_rtalrx.svg" });


            modelBuilder.Entity<MenuOptionTranslation>()
                .HasData(new MenuOptionTranslation { MenuOptionId = 1, DishType = "Child", IsDefaultLanguage=true,Language=Language.EN, Tags=DishTags.Dinner  });
            modelBuilder.Entity<MenuOptionTranslation>()
                .HasData(new MenuOptionTranslation { MenuOptionId = 2, DishType = "Adult", IsDefaultLanguage = true, Language = Language.EN, Tags = DishTags.Dinner });
            modelBuilder.Entity<MenuOptionTranslation>()
                .HasData(new MenuOptionTranslation { MenuOptionId = 3, DishType = "Vegan", IsDefaultLanguage = true, Language = Language.EN, Tags = DishTags.Dinner });
            modelBuilder.Entity<MenuOptionTranslation>()
                .HasData(new MenuOptionTranslation { MenuOptionId = 4, DishType = "Child", IsDefaultLanguage = true, Language = Language.EN, Tags = DishTags.Dinner });
            modelBuilder.Entity<MenuOptionTranslation>()
                .HasData(new MenuOptionTranslation { MenuOptionId = 5, DishType = "Adult", IsDefaultLanguage = true, Language = Language.EN, Tags = DishTags.Dinner });
            modelBuilder.Entity<MenuOptionTranslation>()
                .HasData(new MenuOptionTranslation { MenuOptionId = 6, DishType = "Vegan", IsDefaultLanguage = true, Language = Language.EN, Tags = DishTags.Dinner });
            modelBuilder.Entity<MenuOptionTranslation>()
                .HasData(new MenuOptionTranslation { MenuOptionId = 7, DishType = "Bambino", IsDefaultLanguage = true, Language = Language.IT, Tags = DishTags.Dinner });
            modelBuilder.Entity<MenuOptionTranslation>()
                .HasData(new MenuOptionTranslation { MenuOptionId = 8, DishType = "Adulta", IsDefaultLanguage = true, Language = Language.IT, Tags = DishTags.Dinner });
            modelBuilder.Entity<MenuOptionTranslation>()
                .HasData(new MenuOptionTranslation { MenuOptionId = 9, DishType = "Vegana", IsDefaultLanguage = true, Language = Language.IT, Tags = DishTags.Dinner });


            modelBuilder.Entity<ReligiousCeremony>()
                .HasData(new ReligiousCeremony
                {
                    Id = 1,
                    LocationId = 1,
                    WeddingId = 1,
                    StartDate = new DateTime(2025, 1, 1, 10, 0, 0, DateTimeKind.Utc),
                    EndDate = new DateTime(2025, 2, 1, 12, 45, 0, DateTimeKind.Utc),
                });
            modelBuilder.Entity<ReligiousCeremony>()
                .HasData(new ReligiousCeremony
                {
                    Id = 2,
                    LocationId = 3,
                    WeddingId = 2,
                    StartDate = new DateTime(2025, 6, 1, 10, 0, 0, DateTimeKind.Utc),
                    EndDate = new DateTime(2025, 6, 2, 12, 30, 0, DateTimeKind.Utc),
                });
            modelBuilder.Entity<ReligiousCeremony>()
                .HasData(new ReligiousCeremony
                {
                    Id = 3,
                    LocationId = 6,
                    WeddingId = 3,
                    StartDate = new DateTime(2025, 2, 26, 10, 0, 0, DateTimeKind.Utc),
                    EndDate = new DateTime(2025, 2, 26, 14, 0, 0, DateTimeKind.Utc),
                });
            modelBuilder.Entity<ReligiousCeremonyTranslation>()
                .HasData(new ReligiousCeremonyTranslation
                {
                    ReligiousCeremonyId = 2,
                    Language = Language.NO,
                    Description = "Kom til vår sermoni der vi sier våre løfter",
                    IsDefaultLanguage = true,
                });
            modelBuilder.Entity<ReligiousCeremonyTranslation>()
                .HasData(new ReligiousCeremonyTranslation
                {
                    ReligiousCeremonyId = 2,
                    Language = Language.EN,
                    Description = "Come to our ceremony where we take our vows",
                    IsDefaultLanguage = false,
                });
            modelBuilder.Entity<ReligiousCeremonyTranslation>()
                .HasData(new ReligiousCeremonyTranslation
                {
                    ReligiousCeremonyId = 1,
                    Language = Language.EN,
                    Description = "LAS VEGAS Baby, come and meet ous for the church!",
                    IsDefaultLanguage = true,
                });
            modelBuilder.Entity<ReligiousCeremonyTranslation>()
                .HasData(new ReligiousCeremonyTranslation
                {
                    ReligiousCeremonyId = 1,
                    Language = Language.NO,
                    Description = "LAS VEGAS lækkra, kom å møt oss i kjerka!",
                    IsDefaultLanguage = false,
                });
                 modelBuilder.Entity<ReligiousCeremonyTranslation>()
                .HasData(new ReligiousCeremonyTranslation
                {
                    ReligiousCeremonyId = 3,
                    Language = Language.IT,
                    Description = "Il Matrimonio a Chiesa!",
                    IsDefaultLanguage = true,
                });

            modelBuilder.Entity<Post>()
                .HasData(new Post() { Id = 1, AuthorId = new Guid("12345678-1234-1234-1234-123412341231"), WeddingId = 1 });
            modelBuilder.Entity<PostTranslation>()
                .HasData(new PostTranslation() { PostId = 1 ,Language = Language.EN, IsDefaultLanguage = true, Title = "PARTY!", Body = "Funky times, we will have loads of dance and party!!" });

            modelBuilder.Entity<Post>()
                .HasData(new Post() { Id = 2, AuthorId = new Guid("12345678-1234-1234-1234-123412341233"), WeddingId = 2 });
            modelBuilder.Entity<PostTranslation>()
             .HasData(new PostTranslation() { PostId = 2, Language = Language.EN, IsDefaultLanguage = true, Title = "Gaveliste", Body="Hei, vi ønsker oss disse gavene: - Penger, Gavekort" });

            modelBuilder.Entity<Post>()
              .HasData(new Post() { Id = 3, AuthorId = new Guid("12345678-1234-1234-1234-123412341231"), WeddingId = 3 });
            modelBuilder.Entity<PostTranslation>()
             .HasData(new PostTranslation() { PostId = 3, Language = Language.IT, IsDefaultLanguage = true, Title = "Matrimonio invernale", Body = "Ciao tutti amici!" });
            modelBuilder.Entity<PostTranslation>()
                 .HasData(new PostTranslation() { PostId = 3, Language = Language.EN, IsDefaultLanguage = false, Title = "Winter Marriage", Body = "Hello there friends" });

            modelBuilder.Entity<PostImage>()
                .HasData(new PostImage() { Id = 1, PostId = 1, URI = "https://upload.wikimedia.org/wikipedia/commons/2/21/Johnny_Depp_2020.jpg", Role = "MainImage", Size = "245kb" });
            modelBuilder.Entity<PostImage>()
                .HasData(new PostImage() { Id = 2, PostId = 1, URI = "https://upload.wikimedia.org/wikipedia/commons/thumb/e/e3/Johnny_Depp%27s_signature.svg/512px-Johnny_Depp%27s_signature.svg.png", Role = "SideImage", Size = "15kb" });
            modelBuilder.Entity<PostImage>()
                .HasData(new PostImage() { Id = 3, PostId = 2, URI = "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d8/Vinmonopolets_logo.jpg/600px-Vinmonopolets_logo.jpg", Role = "SideImage", Size = "15kb" });
            modelBuilder.Entity<PostImage>()
                .HasData(new PostImage() { Id = 4, PostId = 2, URI = "https://upload.wikimedia.org/wikipedia/commons/9/9d/Red_Christmas_present_on_white_background.jpg", Role = "MainImage", Size = "155kb" });
            modelBuilder.Entity<PostImage>()
                .HasData(new PostImage() { Id = 5, PostId = 3, URI = "https://upload.wikimedia.org/wikipedia/commons/9/9d/Red_Christmas_present_on_white_background.jpg", Role = "MainImage", Size = "155kb" });

            modelBuilder.Entity<RSVP>()
                .HasData(new RSVP() { Id = 1, WeddingId = 1, Status = RSVPStatus.Accepted, SignerId = new Guid("12345678-1234-1234-1234-123412341235"), Deadline = new DateTime(2025, 6, 1, 10, 0, 0, DateTimeKind.Utc), NumberOfGuests = 1, OtherDietaryRequirements = "None" });
            modelBuilder.Entity<RSVPTranslation>()
                .HasData(new RSVPTranslation() { Body = "Come to my wedding, there will be free alcohol", Language = Language.EN, IsDefaultLanguage = true, RSVPId=1 });

            modelBuilder.Entity<RSVP>()
               .HasData(new RSVP() { Id = 2, WeddingId = 1, SignerId = new Guid("12345678-1234-1234-1234-123412341231"), Deadline = new DateTime(2025, 6, 1, 10, 0, 0, DateTimeKind.Utc), NumberOfGuests = 1, Status = RSVPStatus.Accepted, OtherDietaryRequirements = "None" });
            modelBuilder.Entity<RSVPTranslation>()
                .HasData(new RSVPTranslation() { Body = "Come to my wedding, there will be free alcohol", Language = Language.EN, IsDefaultLanguage = true, RSVPId=2 });

            modelBuilder.Entity<RSVP>()
               .HasData(new RSVP() { Id = 3, WeddingId = 1, SignerId = new Guid("12345678-1234-1234-1234-123412341232"), Deadline = new DateTime(2025, 6, 1, 10, 0, 0, DateTimeKind.Utc), NumberOfGuests = 1, Status = RSVPStatus.Accepted, OtherDietaryRequirements = "None" });
            modelBuilder.Entity<RSVPTranslation>()
                .HasData(new RSVPTranslation() { Body = "Come to my wedding, there will be free alcohol", Language = Language.EN, IsDefaultLanguage = true, RSVPId = 3 });

            modelBuilder.Entity<RSVP>()
                .HasData(new RSVP() { Id = 4, WeddingId = 2, SignerId = new Guid("12345678-1234-1234-1234-123412341236"), Deadline = new DateTime(2026, 6, 1, 10, 0, 0, DateTimeKind.Utc), NumberOfGuests = 1, Status = RSVPStatus.Pending, OtherDietaryRequirements = "None" });
            modelBuilder.Entity<RSVPTranslation>()
                .HasData(new RSVPTranslation() { Body = "Kom til det fantastiske brylluppet mitt", Language = Language.NO, IsDefaultLanguage = true, RSVPId = 4 });
            modelBuilder.Entity<RSVP>()
                .HasData(new RSVP() { Id = 5, WeddingId = 2, SignerId = new Guid("12345678-1234-1234-1234-123412341233"), Deadline = new DateTime(2026, 6, 1, 10, 0, 0, DateTimeKind.Utc), NumberOfGuests = 1, Status = RSVPStatus.Accepted, OtherDietaryRequirements = "None" });
            modelBuilder.Entity<RSVPTranslation>()
                .HasData(new RSVPTranslation() { Body = "Kom til det fantastiske brylluppet mitt", Language = Language.NO, IsDefaultLanguage = true, RSVPId = 5 });
            modelBuilder.Entity<RSVP>()
                .HasData(new RSVP() { Id = 6, WeddingId = 2, SignerId = new Guid("12345678-1234-1234-1234-123412341234"), Deadline = new DateTime(2026, 6, 1, 10, 0, 0, DateTimeKind.Utc), NumberOfGuests = 1, Status = RSVPStatus.Accepted, OtherDietaryRequirements = "None" });
            modelBuilder.Entity<RSVPTranslation>()
                .HasData(new RSVPTranslation() { Body = "Kom til det fantastiske brylluppet mitt", Language = Language.NO, IsDefaultLanguage = true, RSVPId = 6 });
            modelBuilder.Entity<RSVP>()
               .HasData(new RSVP() { Id = 7, WeddingId = 3, SignerId = new Guid("12345678-1234-1234-1234-123412341231"), Deadline = new DateTime(2025, 1, 1, 10, 0, 0, DateTimeKind.Utc), NumberOfGuests = 1, Status = RSVPStatus.Pending, OtherDietaryRequirements = "None" });
            modelBuilder.Entity<RSVPTranslation>()
                .HasData(new RSVPTranslation() { Body = "Benvenuto!", Language = Language.IT, IsDefaultLanguage = true, RSVPId = 7 });
            modelBuilder.Entity<RSVPTranslation>()
                .HasData(new RSVPTranslation() { Body = "Welcome!", Language = Language.EN, IsDefaultLanguage = false, RSVPId = 7 });
            modelBuilder.Entity<MenuOrder>()
                .HasData(new MenuOrder() { Id = 1, MenuOptionId = 1, RSVPId = 6, Name = "Per Gynt", Alergens = Allergen.Almonds + "," + Allergen.Wheat });
            modelBuilder.Entity<MenuOrder>()
                .HasData(new MenuOrder() { Id = 2, MenuOptionId = 1, RSVPId = 6, Name = "Tore Gynt", Alergens = Allergen.Crustaceans + "," + Allergen.Eggs });
            modelBuilder.Entity<MenuOrder>()
                .HasData(new MenuOrder() { Id = 3, MenuOptionId = 2, RSVPId = 6, Name = "Fetter Gynt", Alergens = "" });




            return modelBuilder;
        }
    }
}
