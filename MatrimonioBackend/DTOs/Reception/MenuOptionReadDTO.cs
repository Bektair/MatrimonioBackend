namespace MatrimonioBackend.DTOs.Reception
{
    public class MenuOptionReadDTO
    {
        public int Id { get; set; }
        public string DishType { get; set; }
        public string Tags { get; set; }

        public string Image { get; set; }
        public string Language { get; set; }
        public bool IsDefaultLanguage { get; set; }


    }
}
