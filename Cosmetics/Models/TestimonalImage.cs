using Cosmetics.Models.Customer;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cosmetics.Models
{
    public class TestimonalImageVM:BaseEntity
    {
        public string? Name { get; set; }
        public string? Title { get; set; }
     

        public string? Description { get; set; }
        public string? FullName { get; set; }
        public string? FilePath { get; set; }

       
        [NotMapped]
        public IFormFile? Images { get; set; }        
    }
}
