using Cosmetics.Areas.Admin.ViewModels.Header;
using Cosmetics.DAL;
using Cosmetics.Helper;
using Cosmetics.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cosmetics.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class HeaderController : Controller
    {
        readonly private AppDbContext _context;
        readonly private IFileService _fileService;

        public HeaderController(AppDbContext context, IFileService fileService)
        {
            _context = context;
            _fileService = fileService;
        }
        public async Task<IActionResult> Index()
        {
            List<Header> headers = await _context.Headers.ToListAsync();
            return View(headers);
        }
        public async Task<IActionResult> Details(int id)
        {
            var header = await _context.Headers.FindAsync(id);
            if (header == null) return NotFound();
            return View(header);
        }

        public async Task<IActionResult> Create()
        {
          
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateHeaderVM model)
        {
            _fileService.IsImage(model.FormFile);
            _fileService.CheckSize(model.FormFile, 250);
            var newFile = await _fileService.UploadAsync(model.FormFile);
            Header header = new Header()
            {
                Title = model.Title,
                Content = model.Content,
                Description = model.Description,
                FilePath = newFile
            };

            await _context.AddAsync(header);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        public async Task<IActionResult> Update(int id)
        {
            var header = await _context.Headers.FindAsync(id);
            if (header == null) return NotFound();
            UpdateHeaderVM updateHeaderVM = new UpdateHeaderVM()
            {
                Id = header.Id,
                Title = header.Title,
                Description = header.Description,
                Content = header.Content,
                FilePath = header.FilePath,
                OrderHeader= header.OrderHeader,

            };

            return View(updateHeaderVM);

        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateHeaderVM model)
        {


            var header = await _context.Headers.FindAsync(model.Id);
            if (header == null) return BadRequest();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "\\assets\\img\\", header.FilePath);
            _fileService.Delete(path);
            if (model.FormFile != null)
            {

                _fileService.IsImage(model.FormFile);
                _fileService.CheckSize(model.FormFile, 250);
                var newFile = await _fileService.UploadAsync(model.FormFile);
                header.FilePath = newFile;
            }

            header.OrderHeader = model.OrderHeader;
           
            header.Title=model.Title;
            header.Content = model.Content;
            header.Description = model.Description;

            return RedirectToAction(nameof(Index));

        }

        [HttpPost]
        public async Task<IActionResult> Delete (int id)
        {
            var header = await _context.Headers.FindAsync(id);
            var path = Path.Combine(Directory.GetCurrentDirectory(), "\\assets\\images\\", header.FilePath);
            _fileService.Delete(path);
            _context.Headers.Remove(header);
            return RedirectToAction(nameof(Index));
        }
    }
}

