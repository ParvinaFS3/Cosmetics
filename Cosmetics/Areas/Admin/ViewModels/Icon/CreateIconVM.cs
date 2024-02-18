using System.ComponentModel.DataAnnotations.Schema;

namespace Cosmetics.Areas.Admin.ViewModels.Icon
{
    public class CreateIconVM
    {
        public string? FilePath { get; set; }

        [NotMapped]
        public IFormFile File { get; set; }
    }
}
