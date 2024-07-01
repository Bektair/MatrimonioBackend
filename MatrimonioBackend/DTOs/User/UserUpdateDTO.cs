namespace MatrimonioBackend.DTOs.User
{
    public class UserUpdateDTO
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public string? ProfilePicture { get; set; }

        public string Email { get; set; }

        public string? Nickname { get; set; }

        public bool Email_Verified { get; set; }
    }
}
