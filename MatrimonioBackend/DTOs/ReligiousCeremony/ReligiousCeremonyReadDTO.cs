
using MatrimonioBackend.DTOs.Location;

namespace MatrimonioBackend.DTOs.ReligiousCeremony
{
    public class ReligiousCeremonyReadDTO
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string Description { get; set; }
        public string Language { get; set; }
        public bool IsDefaultLanguage { get; set; }


        //Nav properties
        public LocationReadDTO Location { get; set; }

        public int WeddingId { get; set; }
    }


}

