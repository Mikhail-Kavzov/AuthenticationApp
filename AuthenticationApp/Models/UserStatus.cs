using System.ComponentModel.DataAnnotations;

namespace AuthenticationApp.Models
{
    public enum UserStatus
    {
        [Display(Name = "Active")]
        Active,
        [Display(Name = "Blocked")]
        Blocked,
        [Display(Name = "Deleted")]
        Deleted,
    }
}
