using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cosmetics.Areas.Admin.ViewModels.ProductWholeLook
{
    public class CreateProductWholeLookVM
    {
        [Required]
        public string? Title { get; set; }

        [Required]
        public string? Description { get; set; }


        public string? FilePath { get; set; }

        [NotMapped]
        public IFormFile FormFile { get; set; }
    }
}
