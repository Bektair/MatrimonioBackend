namespace MatrimonioBackend.Models
{
    public class MenuOptionTranslation
    {
        public string Language { get; set; }
        public bool IsDefaultLanguage { get; set; }
        public string DishType { get; set; }
        public string Tags { get; set; }
        public int MenuOptionId { get; set; }
        public MenuOption MenuOption { get; set; }
    }
}
