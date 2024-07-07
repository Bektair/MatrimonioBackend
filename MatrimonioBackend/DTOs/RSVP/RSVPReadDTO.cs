using MatrimonioBackend.DTOs.User;
using MatrimonioBackend.Models;

namespace MatrimonioBackend.DTOs.RSVP
{
    public class RSVPReadDTO
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public DateTime Deadline { get; set; }

        public string Status { get; set; }
        public int NumberOfGuests { get; set; }
        public string OtherDietaryRequirements { get; set; }

        public UserGetDTO Signer { get; set; }
        public IEnumerable<MenuOrderReadDTO> MenuOrders { get; set; }



    }
}
