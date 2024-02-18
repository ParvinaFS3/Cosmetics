using Cosmetics.Areas.Admin.ViewModels.Header;
using Cosmetics.Areas.Admin.ViewModels.Message;
using Cosmetics.Areas.Admin.ViewModels.TestimonalImage;
using Cosmetics.DAL;
using Cosmetics.Helper;
using Cosmetics.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cosmetics.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class TestimonalImageController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IFileService _fileService;

        public TestimonalImageController(AppDbContext context, IFileService fileService)
        {
            _context = context;
            _fileService = fileService;
        }

        public async Task<IActionResult> Index()
        {

            List<TestimonalImageVM> testimonalImages = await _context.TestimonalImages.ToListAsync();
            return View(testimonalImages);
        }
        public async Task<IActionResult> Details(int id)
        {
            var testimonalImage = await _context.TestimonalImages.FindAsync(id);
            if (testimonalImage == null) return NotFound();
            return View(testimonalImage);
        }
        public async Task<IActionResult> Create()
        {

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateTestimonalImageVM model)
        {
            _fileService.IsImage(model.Images);
            _fileService.CheckSize(model.Images, 250);
            var newFile = await _fileService.UploadAsync(model.Images);
            TestimonalImageVM testimonalImagevm = new TestimonalImageVM()
            {
                Name = model.Name,
                Title = model.Title,          
                Description = model.Description,
                FullName = model.FullName,
                FilePath = newFile
            };

            await _context.AddAsync(testimonalImagevm);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        public async Task<IActionResult> Update(int id)
        {
            var testimonalImage = await _context.TestimonalImages.FindAsync(id);
            if (testimonalImage == null) return NotFound();
            UpdateTestimonalImageVM updateTestimonalImagevm = new UpdateTestimonalImageVM()
            {
                Id = testimonalImage.Id,
                Title = testimonalImage.Title,
                Description = testimonalImage.Description,
                FullName = testimonalImage.FullName,
                FilePath = testimonalImage.FilePath,
             

            };

            return View(updateTestimonalImagevm);

        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateTestimonalImageVM model)
        {


            var testimonalImage = await _context.TestimonalImages.FindAsync(model.Id);
            if (testimonalImage == null) return BadRequest();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "\\assets\\img\\", testimonalImage.FilePath);
            _fileService.Delete(path);
            if (model.Images != null)
            {

                _fileService.IsImage(model.Images);
                _fileService.CheckSize(model.Images, 250);
                var newFile = await _fileService.UploadAsync(model.Images);
                testimonalImage.FilePath = newFile;
            }

            testimonalImage.Name= model.Name;

            testimonalImage.Title = model.Title;
            testimonalImage.FullName= model.FullName;
            testimonalImage.Description = model.Description;

            return RedirectToAction(nameof(Index));

        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var testimonalImage = await _context.TestimonalImages.FindAsync(id);
            var path = Path.Combine(Directory.GetCurrentDirectory(), "\\assets\\images\\", testimonalImage.FilePath);
            _fileService.Delete(path);
            _context.TestimonalImages.Remove(testimonalImage);
            return RedirectToAction(nameof(Index));
        }









    }




}

