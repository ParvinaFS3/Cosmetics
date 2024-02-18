using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cosmetics.Areas.Admin.ViewModels.Header
{
    public class CreateHeaderVM
    {
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Content { get; set; }
        [Required]
        public string? Description { get; set; }


        [Required]
        public int OrderHeader {  get; set; }   

        public string? FilePath { get; set; }
        [Required]
        [NotMapped]
        public IFormFile? FormFile { get; set; }
    }
}
