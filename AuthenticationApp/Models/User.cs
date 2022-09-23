namespace AuthenticationApp.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? UserEmail { get; set; }
        public string? Password { get; set; }
        public DateTime RegistryData { get; set; }
        public DateTime LastLogin { get; set; }
        public UserStatus Status { get; set; }
    }
}
