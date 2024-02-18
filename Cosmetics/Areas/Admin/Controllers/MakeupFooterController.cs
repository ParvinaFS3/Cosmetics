using Cosmetics.Areas.Admin.ViewModels.Header;
using Cosmetics.Areas.Admin.ViewModels.MakeupFooterVM;
using Cosmetics.DAL;
using Cosmetics.Helper;
using Cosmetics.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cosmetics.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class MakeupFooterController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IFileService _fileService;

        public MakeupFooterController(AppDbContext context, IFileService fileService)
        {
            _context = context;
            _fileService = fileService;
        }
        public async Task<IActionResult> Index()
        {
            var makeupfooter = await _context.MakeupFooter.ToListAsync();
            return View(makeupfooter);
        }
        public async Task<IActionResult> Details(int id)
        {
            var makeupfooter = await _context.MakeupFooter.FindAsync(id);
            if (makeupfooter == null) return NotFound();
            return View(makeupfooter);
        }
        public async Task<IActionResult> Create()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateMakeupFooterVM model)
        {
            _fileService.IsImage(model.formFile);
            _fileService.CheckSize(model.formFile, 250);
            var newFile = await _fileService.UploadAsync(model.formFile);
            MakeupFooter makeupFooter = new MakeupFooter()
            {
                Title = model.Title,
               
                FilePath = newFile
            };

            await _context.AddAsync(makeupFooter);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        public async Task<IActionResult> Update(int id)
        {
            var makeupFooter = await _context.MakeupFooter.FindAsync(id);
            if (makeupFooter == null) return NotFound();
            UpdateMakeupFooterVM updateMakeupFooterVM = new UpdateMakeupFooterVM()
            {
                Id = makeupFooter.Id,
                Title = makeupFooter.Title,
                FilePath = makeupFooter.FilePath,
            };

            return View(updateMakeupFooterVM);

        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateMakeupFooterVM model)
        {


            var makeupFooter = await _context.MakeupFooter.FindAsync(model.Id);
            if (makeupFooter == null) return BadRequest();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "\\assets\\img\\", makeupFooter.FilePath);
            _fileService.Delete(path);
            if (model.formFile != null)
            {

                _fileService.IsImage(model.formFile);
                _fileService.CheckSize(model.formFile, 250);
                var newFile = await _fileService.UploadAsync(model.formFile);
                makeupFooter.FilePath = newFile;
            }



            makeupFooter.Title = model.Title;
            return RedirectToAction(nameof(Index));

        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var makeupFooter = await _context.MakeupFooter.FindAsync(id);
            var path = Path.Combine(Directory.GetCurrentDirectory(), "\\assets\\images\\", makeupFooter.FilePath);
            _fileService.Delete(path);
            _context.MakeupFooter.Remove(makeupFooter);
            return RedirectToAction(nameof(Index));
        }

    }
}
