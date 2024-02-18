using Cosmetics.Models.Customer;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cosmetics.Models
{
    public class Header:BaseEntity
    {
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Content { get; set; }
        [Required]
        public string? Description { get; set; }

        public int   OrderHeader { get; set; }
        public string? FilePath { get; set; }

        [NotMapped] 
        public IFormFile FormFile { get; set;}

    }
}
