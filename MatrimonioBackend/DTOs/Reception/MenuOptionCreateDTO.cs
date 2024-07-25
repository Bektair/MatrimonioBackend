namespace MatrimonioBackend.DTOs.Reception
{
    public class MenuOptionCreateDTO
    {
        public string DishType { get; set; }
        public string Tags { get; set; }

        public string Image { get; set; }
        public string Language { get; set; }
        public bool IsDefaultLanguage { get; set; }
    }
}
