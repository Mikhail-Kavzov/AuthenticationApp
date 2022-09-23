using System.ComponentModel.DataAnnotations;

namespace AuthenticationApp.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Enter Name")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Enter Email")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Enter Password")]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Passwords are not equal")]
        public string PasswordConfirm { get; set; } = null!;
    }
}
