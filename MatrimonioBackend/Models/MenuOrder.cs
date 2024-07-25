namespace MatrimonioBackend.Models
{
    public class MenuOrder
    {
        public int Id { get; set; }
        public RSVP RSVP { get; set; }
        public int RSVPId { get; set; }
        public string Name { get; set; }
        public string Alergens { get; set; }
        public MenuOption MenuOption { get; set; }
        public int MenuOptionId { get; set; }

    }
}
