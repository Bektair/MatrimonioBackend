namespace MatrimonioBackend.DTOs.User
{
    public class UserGetDTO
    {
        public Guid Id { get; set; }

        public string Username { get; set; }

        public string Keycloakid { get; set; }

        public string Email { get; set; }

    }
}
