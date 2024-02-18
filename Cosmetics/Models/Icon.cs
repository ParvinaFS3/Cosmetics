using Cosmetics.Models.Customer;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cosmetics.Models
{
    public class Icon : BaseEntity
    {

        public string? FilePath { get; set; }

        [NotMapped]
        public IFormFile? File { get; set; }

    }
}
