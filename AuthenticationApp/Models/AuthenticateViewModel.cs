using System.ComponentModel.DataAnnotations;

namespace AuthenticationApp.Models
{
    public class AuthenticateViewModel
    {
        [Required(ErrorMessage = "Enter Email")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Enter Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
    }
}
