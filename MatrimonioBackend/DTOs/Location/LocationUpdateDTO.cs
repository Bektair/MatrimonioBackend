namespace MatrimonioBackend.DTOs.Location
{
    public class LocationUpdateDTO
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public string Placename { get; set; }
        public string Address { get; set; }
        public string? Image { get; set; }
    }
}
