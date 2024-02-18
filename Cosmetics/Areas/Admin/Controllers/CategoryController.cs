

using Cosmetics.Areas.Admin.ViewModels.Category;
using Cosmetics.DAL;
using Cosmetics.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cosmetics.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
  
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            
            List<Category> category = await _context.Categories.Include(a=>a.Products).Select(c=> new Category()
            {
                Id= c.Id,
                Name=c.Name,
                Count = c.Products.Where(p => p.CategoryId == c.Id).Count()


                                         
            }).ToListAsync();
            return View(category);
        }

        public async Task<IActionResult> Details(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return NotFound();
            return View(category);
        }

        public async Task<IActionResult> Create()
        => View();
        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryVM model)
        {
            Category category = new Category()
            {
                Name=model.Name

            };
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update (int id)
        {

            var category = await _context.Categories.FindAsync(id);
            if(category==null) return NotFound();

            UpdateCategoryVM updateCategoryVM = new() { id=category.Id , Name=category.Name };


            return View(updateCategoryVM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id , UpdateCategoryVM model)
        {
            if (id == model.id) return BadRequest();
            var category = await _context.Categories.FindAsync(model.id);

            category.Name=model.Name;

            _context.Categories.Update(category);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]

        public async Task<IActionResult> Delete(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if(category==null) return BadRequest();
            return RedirectToAction(nameof(Index));
        }
    }
}
