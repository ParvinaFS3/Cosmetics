using Cosmetics.DAL;
using Cosmetics.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cosmetics.Controllers
{
    public class HomeController : Controller
    {
        readonly private AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var header = await _context.Headers.ToListAsync();
            var product = await _context.Products.Include(a=>a.Category).Take(8).ToListAsync();
            var productWhole = await _context.ProductWholeLook.FirstOrDefaultAsync();
            var service = await _context.Services.ToListAsync();
            HomeIndexVM vm = new HomeIndexVM()
            {

                Headers = header.FirstOrDefault(),
                Products =product.ToList(),
                ProductWholeLook=productWhole,
                Services= service
            };

            return View(vm);
        }
    }
}
