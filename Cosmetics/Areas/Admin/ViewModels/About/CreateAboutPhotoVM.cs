using Cosmetics.Areas.Admin.ViewModels.Product;
using Cosmetics.Models;
using a = Cosmetics.Models;
namespace Cosmetics.Areas.Admin.ViewModels.About
{
    public class CreateAboutPhotoVM
    {
        public List<a.AboutPhoto>?  Photos { get; set; }
        public CreateAboutVM CreateAvboutVM { get; set; }

    }
}
