using Cosmetics.DAL;
using Cosmetics.Models;
using Cosmetics.ViewModels.Shop;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;

namespace Cosmetics.Controllers
{
    public class ShopController : Controller
    {
        private readonly AppDbContext _context;

        public ShopController(AppDbContext  context)
        {
            _context=context;
        }


        public async Task<IActionResult> Index(ProductIndexVM model)
        {
            var category = await _context.Categories.ToListAsync();
            var products = FilterByTitle(model.Title);
            products = FilterByQuantity(model.MinQuantity, model.MaxQuantity, products);
            products = FilterByCategory(model.CategoryId, products);
            if (model.FilterByPriceLowToHigh)
            {
                products = FilterByPriceLowToHigh(products);
            }
            else if (model.FilterByPriceHighToLow)
            {
                products = FilterByPriceHighToLow(products);
            }
            else if (model.FilterByLatestProduct)
            {
                products = FilterByLatestProduct(products);
            }


            model = new ProductIndexVM
            {
                Products = products
                .Include(ct => ct.Category)
                .ToList(),
                Categories =category

            };

            return View(model);
        }
        private IQueryable<Product> FilterByTitle(string? title)
        {
            return _context.Products.Where(p => !string.IsNullOrEmpty(title) ? p.Name.Contains(title) : true);
        }
        private IQueryable<Product> FilterByQuantity(int? minQuantity, int? maxQuantity, IQueryable<Product> products)
        {

            return products
                    .Where(p => (minQuantity != null ? p.Quantity >= minQuantity : true) && (maxQuantity != null ? p.Quantity <= maxQuantity : true));

        }

        private IQueryable<Product> FilterByCategory(int? categoryId, IQueryable<Product> products)
        {

            return products.Where(p => categoryId != null ? p.CategoryId == categoryId : true);



        }
        private IQueryable<Product> FilterByPriceLowToHigh(IQueryable<Product> products)
        {
            return products.OrderBy(p => p.Price);
        }

        private IQueryable<Product> FilterByPriceHighToLow(IQueryable<Product> products)
        {
            return products.OrderByDescending(p => p.Price);

        }

        private IQueryable<Product> FilterByLatestProduct(IQueryable<Product> products)
        {
            return products.OrderByDescending(p => p.CreatedAt);

        }


    }
}
