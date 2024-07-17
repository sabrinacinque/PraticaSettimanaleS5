namespace PraticaSettimanaleS5.Models
{
    public class ApplicationUser
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
        public string? FriendlyName { get; set; }
    }
}
