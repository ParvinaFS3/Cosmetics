using Cosmetics.Models.Customer;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cosmetics.Models
{
    public class MakeupFooter:BaseEntity
    {
        public string? Title { get; set; }

        public string? FilePath  { get; set; }

        [NotMapped]
        public IFormFile? formFile { get; set; } 
    }
}
