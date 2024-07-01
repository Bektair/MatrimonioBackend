
namespace MatrimonioBackend.Models
{
    public class MenuOption
    {
        public int Id { get; set; }
        public string DishType { get; set; }
        public string Tags { get; set; }
        public string Image { get; set; }

        //Nav
        public Reception Reception { get; set; }
        public int ReceptionId { get; set; }
        public ICollection<MenuOrder> MenuOrders { get; set; } = new List<MenuOrder>();


    }
}
