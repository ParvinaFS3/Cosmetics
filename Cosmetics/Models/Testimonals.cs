
using Cosmetics.Models.Customer;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cosmetics.Models
{
    public class Testimonals: BaseEntity
    {

        public string? FilePath { get; set; }
        public ICollection<TestimonalBackground> Backgrounds { get; set; }  
        [NotMapped]
        public IFormFile? File { get; set; }
    }
}
