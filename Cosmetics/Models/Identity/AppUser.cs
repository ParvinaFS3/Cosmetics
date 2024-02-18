using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Cosmetics.Models.Identity
{
    public class AppUser:IdentityUser
    {
        [Required,MaxLength(100)]
        public string NameSurName { get; set; }

        //[Required, DataType(DataType.Password)] 
        //public string Password { get; set; }
        //[ Compare(nameof(Password))]
        //public string? ConfirmPassword { get; set; }

        public int? BasketId { get; set; }   
        public Basket? Basket { get; set; }
    }
}
