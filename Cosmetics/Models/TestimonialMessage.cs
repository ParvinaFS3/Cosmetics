
using Cosmetics.Models.Customer;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cosmetics.Models
{
    public class TestimonialMessage:BaseEntity
    {
        public string  FullName { get; set; }
        public string Message { get; set; }

        public string FilePath { get; set; }

        [NotMapped]

        public IFormFile formFile { get; set; } 
    }
}
