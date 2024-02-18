using e=Cosmetics.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cosmetics.Areas.Admin.ViewModels.Testimonal
{
    public class CreateTestimonalVM
    {
        public string FilePath { get; set; }
        public string ProfileImage { get; set; }

        public int ProductId { get; set; }

        public e.Product Product { get; set; }

       
        public ICollection<IFormFile> Backgrounds { get; set; }
        [NotMapped]
        public IFormFile File { get; set; }
    }
}
