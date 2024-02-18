using Cosmetics.Areas.Admin.ViewModels.ProductWholeLook;
using Cosmetics.DAL;
using Cosmetics.Helper;
using Cosmetics.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cosmetics.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class ProductWholeLookController : Controller
    {
      
            readonly private AppDbContext _context;
            readonly private IFileService _fileService;

            public ProductWholeLookController(AppDbContext context, IFileService fileService)
            {
                _context = context;
                _fileService = fileService;
            }


            public async Task<IActionResult> Index()
            {

            List<ProductWholeLook> productWholeLooks = await _context.ProductWholeLook.ToListAsync();
                return View(productWholeLooks);
            }
            public async Task<IActionResult> Details(int id)
            {
                var ProductWholeLook = await _context.ProductWholeLook.FindAsync(id);
                if (ProductWholeLook == null) return NotFound();
                return View(ProductWholeLook);
            }
            public async Task<IActionResult> Create()
            {
                List<ProductWholeLook> ProductWholeLooks = await _context.ProductWholeLook.ToListAsync();
                if (ProductWholeLooks.Any()) return NotFound();
                return View();
            }
            [HttpPost]
            public async Task<IActionResult> Create(CreateProductWholeLookVM model)
            {
                _fileService.IsImage(model.FormFile);
                _fileService.CheckSize(model.FormFile, 250);
                var newFile = await _fileService.UploadAsync(model.FormFile);
                ProductWholeLook ProductWholeLook = new ProductWholeLook()
                {
                    Title = model.Title,
                    Description = model.Description,
                    FilePath = newFile
                };

                await _context.AddAsync(ProductWholeLook);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            public async Task<IActionResult> Update(int id)
            {
                var ProductWholeLook = await _context.ProductWholeLook.FindAsync(id);

                if (ProductWholeLook == null) return NotFound();
                UpdateProductWholeLookVM updateHeaderVM = new UpdateProductWholeLookVM()
                {
                    Id = ProductWholeLook.Id,
                    Title = ProductWholeLook.Title,
                    Description = ProductWholeLook.Description,
                    FilePath = ProductWholeLook.FilePath,

                };

                return View(updateHeaderVM);

            }
            [HttpPost]
            public async Task<IActionResult> Update(UpdateProductWholeLookVM model)
            {


                var ProductWholeLook = await _context.ProductWholeLook.FindAsync(model.Id);
                if (ProductWholeLook == null) return BadRequest();
                var path = Path.Combine(Directory.GetCurrentDirectory(), "\\assets\\img\\", ProductWholeLook.FilePath);
                _fileService.Delete(path);
                if (model.FormFile != null)
                {

                    _fileService.IsImage(model.FormFile);
                    _fileService.CheckSize(model.FormFile, 250);
                    var newFile = await _fileService.UploadAsync(model.FormFile);
                    ProductWholeLook.FilePath = newFile;


                }

                ProductWholeLook.Description = model.Description;

                return RedirectToAction(nameof(Index));

            }
            [HttpPost]
            public async Task<IActionResult> Delete(int id)
            {
                var ProductWholeLook = await _context.ProductWholeLook.FindAsync(id);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "\\assets\\img\\", ProductWholeLook.FilePath);
                _fileService.Delete(path);
                _context.ProductWholeLook.Remove(ProductWholeLook);
                return RedirectToAction(nameof(Index));
            }
        }
    }

