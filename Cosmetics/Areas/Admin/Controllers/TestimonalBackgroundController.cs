using Cosmetics.Areas.Admin.ViewModels.FeaturedProduct;
using Cosmetics.Areas.Admin.ViewModels.Header;
using Cosmetics.Areas.Admin.ViewModels.TestimonalBackground;
using Cosmetics.DAL;
using Cosmetics.Helper;
using Cosmetics.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cosmetics.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class TestimonalBackgroundController : Controller
    {
        readonly private AppDbContext _context;
        readonly private IFileService _fileService;
        public TestimonalBackgroundController(AppDbContext context , IFileService fileService)
        {
            _context = context;
            _fileService = fileService;

        }
        public async Task<IActionResult> Index()
        {
            var testimonalBackground = await _context.TestimonalBackground
                                        .Include(t => t.Testimonals).ToListAsync();



            return View(testimonalBackground);

        }



        public async Task<IActionResult> Details(int id)
        {
            var testimonalBackground = await _context.TestimonalBackground.FindAsync(id);
            if (testimonalBackground == null) return NotFound();
            return View(testimonalBackground);
        }

        public async Task<IActionResult> Create()
        => View();
        [HttpPost]
        public async Task<IActionResult> Create(CreateTestimonalBackgroundVM model)
        {
            if (!ModelState.IsValid) return View(model);

            _fileService.IsImage(model.formFile);
            _fileService.CheckSize(model.formFile, 250);
            var newFile = await _fileService.UploadAsync(model.formFile);
            var testimonals = await _context.Testimonals.FirstOrDefaultAsync();
            TestimonalBackground testimonalBackground = new()
            {
                TestimonalsId = testimonals.Id             

            };
            await _context.TestimonalBackground.AddAsync(testimonalBackground);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            var testimonalBackground = await _context.TestimonalBackground.FindAsync(id);

            UpdateTestimonalBackgroundVM testimonalBackgroundvm = new UpdateTestimonalBackgroundVM()
            {
                FilePath = testimonalBackground.FilePath,
                formFile = testimonalBackground.formFile
            };



            return View(testimonalBackgroundvm);

        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateTestimonalBackgroundVM model)
        {


            var testimonalBackground = await _context.TestimonalBackground.FindAsync(model.Id);
            if (testimonalBackground == null) return BadRequest();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "\\assets\\img\\", testimonalBackground.FilePath);
            _fileService.Delete(path);
            if (model.formFile != null)
            {

                _fileService.IsImage(model.formFile);
                _fileService.CheckSize(model.formFile, 250);
                var newFile = await _fileService.UploadAsync(model.formFile);
                testimonalBackground.FilePath = newFile;
            }

            testimonalBackground.FilePath = model.FilePath;
            testimonalBackground.formFile = model.formFile;      

            return RedirectToAction(nameof(Index));

        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var  testimonalBackground = await _context.TestimonalBackground.FindAsync(id);
            var path = Path.Combine(Directory.GetCurrentDirectory(), "\\assets\\images\\", testimonalBackground.FilePath);
            _fileService.Delete(path);
            _context.TestimonalBackground.Remove(testimonalBackground);
            return RedirectToAction(nameof(Index));
        }

    }
}
