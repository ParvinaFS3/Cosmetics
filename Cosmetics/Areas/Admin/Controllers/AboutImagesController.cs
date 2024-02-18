using Cosmetics.Areas.Admin.ViewModels.AboutImages;
using Cosmetics.DAL;
using Cosmetics.Helper;
using Cosmetics.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cosmetics.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class AboutImagesController : Controller
    {
        readonly private AppDbContext _context;

        readonly private IFileService _fileService;

        public AboutImagesController(AppDbContext  context, IFileService fileService )
        {
            _context= context;
            _fileService= fileService;
            
        }
        public async Task<IActionResult> Index()
        {
            var aboutImage = await _context.AboutImages.Include(a=>a.Order).ToListAsync();
            return View(aboutImage);
        }
        public async Task<IActionResult> Details(int id)
            => await _context.AboutImages.FindAsync(id) == null ? NotFound() : View();
        [HttpPost]
        public async Task<IActionResult> Create (CreateAboutImagesVM model)
        {
            _fileService.IsImage(model.File);
            _fileService.CheckSize(model.File, 250);
            var newFile = await _fileService.UploadAsync(model.File);

            AboutImage aboutImages = new AboutImage()
            {
                FilePath=newFile,
                Order=model.Order,
            };
            await _context.AddAsync(aboutImages);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));


        }

        public async Task<IActionResult> Update (string id)
        {
            var aboutImage = await _context.AboutImages.FindAsync(id);
            if (aboutImage == null) return NotFound();
            UpdateAboutImagesVM vm = new UpdateAboutImagesVM()
            {
                Id = aboutImage.Id,
                File = aboutImage.File,
                FilePath = aboutImage.FilePath,
                Order = aboutImage.Order
            };

            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Update (UpdateAboutImagesVM model)
        {
            var aboutImage = await _context.AboutImages.FindAsync(model.Id);
            if(aboutImage == null) return BadRequest();

            var path = Path.Combine(Directory.GetCurrentDirectory(), "\\wwwroot\\assets\\images\\");

            _fileService.Delete(path);
            if(model.File != null) {
                _fileService.IsImage(model.File);
                _fileService.CheckSize(model.File , 250);

                var newFile = await _fileService.UploadAsync(model.File);

                aboutImage.FilePath = newFile;

            
            }

            aboutImage.Order = model.Order;

            _context.AboutImages.Update(aboutImage);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete (int id)
        {
            var aboutImage = await _context.AboutImages.FindAsync(id);
            if (aboutImage == null) return BadRequest();

            var path = Path.Combine(Directory.GetCurrentDirectory(), "\\wwwroot\\assets\\images\\");

            _fileService.Delete(path);

            _context.AboutImages.Remove(aboutImage);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }
    }
}