using Cosmetics.Models.Customer;
using Microsoft.Extensions.FileSystemGlobbing;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cosmetics.Models
{
    public class TestimonalBackground: BaseEntity
    {
        public string?  FilePath { get; set; }
        [NotMapped]
        public IFormFile? formFile { get; set; }

        public int TestimonalsId { get; set; }      

        public Testimonals Testimonals { get; set; }        
    }
}
    