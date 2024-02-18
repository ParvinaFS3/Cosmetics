using e=Cosmetics.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cosmetics.Areas.Admin.ViewModels.Testimonal
{
    public class UpdateTestimonalVM
    {
        public int Id { get; set; } 
        public string FilePath { get; set; }
        public string ProfileImage { get; set; }

        public int ProductId { get; set; }

        public e.Product Product { get; set; }

        [NotMapped]
        public ICollection<IFormFile> Backgrounds { get; set; }
        [NotMapped]
        public IFormFile File { get; set; }
    }
}
