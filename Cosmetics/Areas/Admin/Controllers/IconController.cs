using Cosmetics.Areas.Admin.ViewModels.Icon;
using Cosmetics.DAL;
using Cosmetics.Helper;
using Cosmetics.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace Cosmetics.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class IconController : Controller
    {
        readonly private AppDbContext _context;
        readonly private IFileService _fileService;

        public IconController(AppDbContext context, IFileService fileService)
        {
            _context = context;
            _fileService = fileService;
        }
        public async Task<IActionResult> Index()
        {
            var icon = await _context.Icons.ToListAsync();


            return View(icon);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var icon = await _context.Icons.FindAsync(id);
            if (icon == null) return NotFound();
            return View(icon);
        }
        public async Task<IActionResult> Create()
        => View();

        [HttpPost]
        public async Task<IActionResult> Create(CreateIconVM model)
        {
            if (!ModelState.IsValid) return View(model);

            _fileService.IsImage(model.File);
            _fileService.CheckSize(model.File, 250);
            var newFile = await _fileService.UploadAsync(model.File);
            Icon ıcon = new Icon()
            {
                FilePath = newFile
            };
            await _context.Icons.AddAsync(ıcon);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Update(int id)
        {
            var icon = await _context.Icons.FindAsync(id);
            if (icon == null) return NotFound();

            UpdateIconVM vm = new UpdateIconVM()
            {
                Id = icon.Id,
                FilePath = icon.FilePath
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateIconVM model)
        {
            if (!ModelState.IsValid) return View(model);
            var icon = await _context.Icons.FindAsync(model.Id);
            if (icon == null) return NotFound();

            var path = Path.Combine(Directory.GetCurrentDirectory(), "\\wwwroot\\assets\\images", icon.FilePath);

            _fileService.Delete(path);
            if (model.File != null)
            {
                _fileService.IsImage(model.File);
                _fileService.CheckSize(model.File, 250);

                var newFile = await _fileService.UploadAsync(model.File);
                icon.FilePath = newFile;


           
            }
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var icon = await _context.Icons.FindAsync(id);
            if (icon == null) return NotFound();

            _context.Icons.Remove(icon);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
