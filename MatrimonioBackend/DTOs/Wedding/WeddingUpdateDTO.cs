namespace MatrimonioBackend.DTOs.Wedding
{
    public class WeddingUpdateDTO
    {
        public int id { get; set; }
        public string description { get; set; }
        public string dresscode { get; set; }

        public string primaryColor { get; set; }
        public string secoundaryColor { get; set; }
        public string primaryFontColor { get; set; }
        public string secoundaryFontColor { get; set; }

        public string backgroundImage { get; set; }

        public string bodyFont { get; set; }
        public string headingFont { get; set; }
        public string title { get; set; }
        public string picture { get; set; }

    }
}
