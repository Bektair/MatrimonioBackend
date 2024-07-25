namespace MatrimonioBackend.Models
{
    public class ReceptionTranslation
    {
        public string Description { get; set; }
        public bool IsDefaultLanguage { get; set; }
        public string Language { get; set; }

        //Nav
        public Reception Reception { get; set; }
        public int ReceptionId { get; set; }
    }
}
