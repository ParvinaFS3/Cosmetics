using Cosmetics.DAL;
using Cosmetics.Models;
using Cosmetics.Models.Identity;
using Cosmetics.ViewModels.Basket;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cosmetics.Controllers
{
    public class BasketController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _context;

        public BasketController(UserManager<AppUser> userManager,
            AppDbContext context   )
        {
            _userManager = userManager;
            _context = context;

        }


        public async Task<IActionResult> Index()
        {
            var appuser = await _userManager.GetUserAsync(User);
            if (appuser == null) return Unauthorized();

            var basket = await _context.Baskets
                .Include(bp => bp.BasketProducts)
                .ThenInclude(p => p.Product)
                .FirstOrDefaultAsync(b => b.AppUserId == appuser.Id);

            var model = new BasketIndexVM();

            if (basket == null) return View(model);
            foreach (var basketProduct in basket.BasketProducts)
            {
                var product = new BasketProductVM
                {
                    Id = basketProduct.Id,
                    Quantity = basketProduct.Quantity,
                    Name = basketProduct.Product.Name,
                    PhotoPath = basketProduct.Product.FilePath,
                    Price = basketProduct.Product.Price

                };
                model.BasketProducts.Add(product);

            }

            return View(model);
        }



        public async Task<IActionResult> Add(int id)
        {
            var appUser = await _userManager.GetUserAsync(User);
            if (appUser == null) return Unauthorized();

            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();
            var appuserBasket = await _context.Baskets.FirstOrDefaultAsync(b => b.AppUserId == appUser.Id);

            if (appuserBasket == null)
            {

                Basket ppuserBasket = new Basket
                {
                    AppUserId = appUser.Id,

                };

                await _context.Baskets.AddAsync(appuserBasket);
                await _context.SaveChangesAsync();

            }
            var basketProduct = await _context.BasketProducts.FirstOrDefaultAsync(bp => bp.ProductId == id && bp.BasketId == appuserBasket.Id);
            if (basketProduct == null)
            {
                basketProduct = new BasketProduct
                {
                    BasketId = appuserBasket.Id,
                    ProductId = product.Id,
                    Quantity = 1
                };
                await _context.BasketProducts.AddAsync(basketProduct);
            }
            else
            {
                basketProduct.Quantity++;
            }
            await _context.SaveChangesAsync();

            return Ok();

        }

        public async Task<IActionResult> Delete(int id)
        {
            var appUser = await _userManager.GetUserAsync(User);
            if (appUser == null) return Unauthorized();


            var basketProduct = await _context.BasketProducts
                .FirstOrDefaultAsync(bp => bp.Basket.AppUserId == appUser.Id && bp.Id == id);

            if (basketProduct == null) return NotFound();

            _context.BasketProducts.Remove(basketProduct);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}



