using Cosmetics.Models.Customer;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cosmetics.Models
{
    public class AboutPhoto : BaseEntity
    {
        public string? FilePath { get; set; }

        public int AboutId { get; set; }

        public About? About { get; set; }
        public int Order { get; set; }

        [NotMapped]
        public IFormFile? File { get; set; }
    }
}
