using System.ComponentModel.DataAnnotations.Schema;

namespace Cosmetics.Areas.Admin.ViewModels.TestimonalImage
{
    public class UpdateTestimonalImageVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }


        public string Description { get; set; }
        public string FullName { get; set; }
        public string FilePath { get; set; }


        [NotMapped]
        public IFormFile Images { get; set; }
    }
}
