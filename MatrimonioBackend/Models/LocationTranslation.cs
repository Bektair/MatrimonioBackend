namespace MatrimonioBackend.Models
{
    public class LocationTranslation : ITranslation
    {
        //299 Rue le Carr, Fermont, QC G0G 1J0, Canada Fra googlemaps format.
        public string Country { get; set; }
        public string Address { get; set; }

        public string Title { get; set; }
        public string Body { get; set; }

        public string Language { get; set; }
        public bool IsDefaultLanguage { get; set; }

        //NAV
        public Location? Location { get; set; }
        public int LocationId { get; set; }
    }
}
