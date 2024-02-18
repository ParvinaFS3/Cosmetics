using Cosmetics.Areas.Admin.ViewModels.Product;
using Cosmetics.Models;

namespace Cosmetics.Areas.Admin.Views.Product
{
    public class ProductCreateCategoryVM
    {
        public List<Category> Categories { get; set; }  

        public CreateProductVM CreateProductVM { get; set; }
    }
}
