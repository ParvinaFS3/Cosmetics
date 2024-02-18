using Cosmetics.Areas.Admin.ViewModels.FeaturedProduct;
using Cosmetics.Areas.Admin.ViewModels.Header;
using Cosmetics.Areas.Admin.ViewModels.Product;
using Cosmetics.Areas.Admin.Views.Product;
using Cosmetics.DAL;
using Cosmetics.Helper;
using Cosmetics.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace Cosmetics.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class FeaturedProductController : Controller
    {
        readonly private AppDbContext _context;
        readonly private IFileService _fileService;
        public FeaturedProductController(AppDbContext context, IFileService fileService)
        {
            _context = context;
            _fileService = fileService;
        }
        public async Task<IActionResult> Index()
        {
            var featuredProduct = await _context.FeaturedProducts
                                        .Include(a => a.About).ToListAsync();



            return View(featuredProduct);
        }
        public async Task<IActionResult> Details(int id)
        {
            var featuredProduct = await _context.FeaturedProducts.FindAsync(id);
            if (featuredProduct == null) return NotFound();
            return View(featuredProduct);
        }


        public async Task<IActionResult> Create()
        => View();
        [HttpPost]
        public async Task<IActionResult> Create(CreateFeaturedProducttVM model)
        {
            if (!ModelState.IsValid) return View(model);
            var about = await _context.About.FirstOrDefaultAsync();
            FeaturedProduct featuredProduct = new()
            {
                AboutId = about.Id,
                Name = model.Name,


            };
            await _context.FeaturedProducts.AddAsync(featuredProduct);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



        public async Task<IActionResult> Update(int id)
        {
            var featuredProduct = await _context.FeaturedProducts.FindAsync(id);

            UpdateFeaturedProductVM vm = new UpdateFeaturedProductVM()
            {
                Name = featuredProduct.Name
            };

            return View(vm);

        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateFeaturedProductVM model)
        {
            if (!ModelState.IsValid) return View(model);

            var featuredProduct = await _context.FeaturedProducts.FindAsync(model.Id);
            if (featuredProduct == null) return BadRequest();
            featuredProduct.Name = model.Name;

            _context.FeaturedProducts.Update(featuredProduct);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete (int id)
        {
            var featuredProduct = await _context.FeaturedProducts.FindAsync(id);
            if (featuredProduct == null) return BadRequest();
            _context.Remove(featuredProduct);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
