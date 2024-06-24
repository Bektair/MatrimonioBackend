namespace MatrimonioBackend.DTOs.ReligiousCeremony
{
    public class ReligiousCeremonyCreateDTO
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string Description { get; set; }

        //Nav properties
        public int LocationId { get; set; }
        public int? WeddingId { get; set; }
    }
}
