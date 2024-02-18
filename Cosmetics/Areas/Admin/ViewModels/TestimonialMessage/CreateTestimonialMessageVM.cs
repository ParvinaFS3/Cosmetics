using System.ComponentModel.DataAnnotations.Schema;

namespace Cosmetics.Areas.Admin.ViewModels.TestimonialMessage
{
    public class CreateTestimonialMessageVM
    {

        public string FullName { get; set; }
        public string Message { get; set; }

        public string FilePath { get; set; }

        [NotMapped]

        public IFormFile formFile { get; set; }
    }
}
