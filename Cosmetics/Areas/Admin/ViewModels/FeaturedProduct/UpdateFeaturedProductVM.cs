using a = Cosmetics.Models;

namespace Cosmetics.Areas.Admin.ViewModels.FeaturedProduct
{
    public class UpdateFeaturedProductVM
    { 
        public int Id { get; set; }
        public string Name { get; set; }

        public int AboutId { get; set; }

        public a.About About { get; set; }
    }
}
