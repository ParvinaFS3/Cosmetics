using System.ComponentModel.DataAnnotations.Schema;
using a = Cosmetics.Models;
namespace Cosmetics.Areas.Admin.ViewModels.AboutPhoto
{
    public class CreateAboutPhotoVM
    {
        public string FilePath { get; set; }

        public int AboutId { get; set; }

        public a.About About { get; set; }
        public int Order { get; set; }

        [NotMapped]
        public IFormFile File { get; set; }
    }
}
