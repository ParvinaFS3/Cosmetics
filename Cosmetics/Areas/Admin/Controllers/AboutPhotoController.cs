using Cosmetics.Areas.Admin.ViewModels.About;
using Cosmetics.Areas.Admin.ViewModels.FeaturedProduct;
using Cosmetics.DAL;
using Cosmetics.Helper;
using Cosmetics.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cosmetics.Areas.Admin.Controllers
{
    public class AboutPhotoController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IFileService _fileService;

        public AboutPhotoController(AppDbContext context, IFileService fileService)
        {
            _context = context;
            _fileService = fileService;
        }
        public async Task<IActionResult> Index()
        {
            var aboutPhoto = await _context.AboutPhotos.Include(a => a.Order)
                                        .Include(a => a.About).ToListAsync();



            return View(aboutPhoto);

        }
        public async Task<IActionResult> Details(int id)
        {
            var aboutPhoto = await _context.AboutPhotos.FindAsync(id);
            if (aboutPhoto == null) return NotFound();
            return View(aboutPhoto);
        }

        public async Task<IActionResult> Create()
        => View();
        [HttpPost]
        public async Task<IActionResult> Create(CreateAboutPhotoVM model)
        {
 
            return RedirectToAction(nameof(Index));
        }

    }

}
