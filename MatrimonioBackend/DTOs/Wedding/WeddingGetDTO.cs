namespace MatrimonioBackend.DTOs.Wedding
{
    public class WeddingGetDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Dresscode { get; set; }

        public string MainColor { get; set; }
        public string SecoundaryColor { get; set; }
        public string MainFontColor { get; set; }
        public string SecoundaryFontColor { get; set; }

        public string BackgroundImage { get; set; }

        public string BodyFont { get; set; }
        public string HeadingFont { get; set; }

    }
}
