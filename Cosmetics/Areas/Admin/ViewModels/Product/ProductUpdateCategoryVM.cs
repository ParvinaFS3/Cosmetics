using Cosmetics.Areas.Admin.ViewModels.Product;
using Cosmetics.Models;
using E = Cosmetics.Models;
namespace Cosmetics.Areas.Admin.ViewModels
{
    public class ProductUpdateCategoryVM
    {
        public List<E::Category>? Categories { get; set; }

       
        public UpdateProductVM? UpdateProductVm { get; set; }


    }
}
