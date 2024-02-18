using Cosmetics.Models.Customer;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cosmetics.Models
{
    public class AboutImage:BaseEntity
    {
        public string? FilePath { get; set; }        

        public int Order { get; set; }
        [NotMapped]
        public IFormFile? File { get; set; }
    }
}
