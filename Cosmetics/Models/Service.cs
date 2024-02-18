using Cosmetics.Models.Customer;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cosmetics.Models
{
    public class Service : BaseEntity
    {
     
        public string Icon { get; set; }

        [Required]
        public string? Title { get; set; }
       
        [Required]
        public string? Description { get; set; }


       
    }
}
