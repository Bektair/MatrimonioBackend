namespace MatrimonioBackend.Models
{
    public class RSVPTranslation : ITranslation
    {
        public string Body { get; set; }
        public string Language { get; set; }
        public bool IsDefaultLanguage { get; set; }
        public RSVP? RSVP { get; set; }
        public int? RSVPId { get; set; }
    }
}
