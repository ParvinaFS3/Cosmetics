using System.ComponentModel.DataAnnotations;

namespace Cosmetics.ViewModels.Account
{
    public class AccountLoginVM
    {

        [Required]
        public string Username { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        public string? ReturnUrl { get; set; }

    }
}
