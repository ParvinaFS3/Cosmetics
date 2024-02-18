using System.ComponentModel.DataAnnotations.Schema;

namespace Cosmetics.Areas.Admin.ViewModels.AboutImages
{
    public class CreateAboutImagesVM
    {
        public string FilePath { get; set; }

        public int Order { get; set; }
        [NotMapped]
        public IFormFile File { get; set; }
    }
}
