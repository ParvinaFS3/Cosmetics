using System.ComponentModel.DataAnnotations.Schema;

namespace Cosmetics.Areas.Admin.ViewModels.Icon
{
    public class UpdateIconVM
    {
        public int Id { get; set; }
        public string? FilePath { get; set; }

        [NotMapped]
        public IFormFile File { get; set; }
    }
}
