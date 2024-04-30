
namespace MatrimonioBackend.Models
{
    public class MenuOption
    {
        public int Id { get; set; }
        public string DishName { get; set; }
        public string Alergens { get; set; }
        public string Tags { get; set; }

        public string Image { get; set; }

        //Nav
        public Reception Reception { get; set; }
        public int ReceptionId { get; set; }

    }
}
