using Cosmetics.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Cosmetics.Areas.Admin.ViewModels.About
{
    public class CreateAboutVM
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string? Content { get; set; }
        [Required]
        public string Description { get; set; }

        public string? FilePath { get; set; }

        [Required]
        [NotMapped]
        public ICollection<IFormFile> Photos { get; set; }
     
    }
}
