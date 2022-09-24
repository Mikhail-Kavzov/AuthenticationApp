using Microsoft.AspNetCore.Identity;

namespace AuthenticationApp.Models
{
    public class User : IdentityUser
    {
        public DateTime RegistryData { get; set; }
        public DateTime LastLogin { get; set; }
        public UserStatus Status { get; set; }
    }
}
