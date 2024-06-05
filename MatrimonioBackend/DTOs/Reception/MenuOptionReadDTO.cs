namespace MatrimonioBackend.DTOs.Reception
{
    public class MenuOptionReadDTO
    {
        public int Id { get; set; }
        public string DishName { get; set; }
        public string Alergens { get; set; }
        public string Tags { get; set; }

        public string Image { get; set; }
    }
}
