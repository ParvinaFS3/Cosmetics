using Cosmetics.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cosmetics.Areas.Admin.ViewModels.TestimonalBackground
{
    public class CreateTestimonalBackgroundVM
    {

        public string FilePath { get; set; }
        [NotMapped]
        public IFormFile formFile { get; set; }

        public int TestimonalsId { get; set; }

        public Testimonals Testimonals { get; set; }
    }
}
