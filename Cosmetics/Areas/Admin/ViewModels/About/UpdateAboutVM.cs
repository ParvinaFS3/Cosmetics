using Cosmetics.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using a = Cosmetics.Models;
namespace Cosmetics.Areas.Admin.ViewModels.About
{
    public class UpdateAboutVM
    {    public int Id {  get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Content { get; set; }
        [Required]
        public string? Description { get; set; }

        public string? FilePath { get; set; }

        [NotMapped]
        public ICollection<IFormFile>? Photos { get; set; }
       public ICollection<a.FeaturedProduct>? FeaturedProducts { get; set; }
    }
}
