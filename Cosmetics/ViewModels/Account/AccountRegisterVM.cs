using System.ComponentModel.DataAnnotations;

namespace Cosmetics.ViewModels.Account
{
    public class AccountRegisterVM
    {
        [Required, MaxLength(100)]
        public string NameSurname { get; set; }

        [Required, MaxLength(100)]
        public string Username { get; set; }
        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
        [Required, DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }


    }
}
