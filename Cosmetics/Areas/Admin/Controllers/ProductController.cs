using Cosmetics.Areas.Admin.ViewModels;
using Cosmetics.Areas.Admin.ViewModels.Product;
using Cosmetics.Areas.Admin.Views.Product;
using Cosmetics.DAL;
using Cosmetics.Helper;
using Cosmetics.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Cosmetics.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class ProductController : Controller
    {
        readonly private AppDbContext _context;
        readonly private IFileService _fileService;

        public ProductController(AppDbContext context, IFileService fileService)
        {
            _context = context;
            _fileService = fileService;
        }

        public async Task<IActionResult> Index()
        {
            var product = await _context.Products
                                        .Include(a=>a.Category)
                                        
                                        .Include(a=>a.BasketProducts).ToListAsync();

            return View(product);
        }
    

        public async Task<IActionResult> Details(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();
            return View(product);
        }
        public async Task<IActionResult> Create()
        {
            var category = await _context.Categories.Include(a => a.Products).ToListAsync();
            var product = new CreateProductVM();

            ProductCreateCategoryVM productCreateCategoryVM = new ProductCreateCategoryVM()
            {
                CreateProductVM=product,
                Categories=category
            };
            return View(productCreateCategoryVM);

        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateCategoryVM model)
        {
            _fileService.IsImage(model.CreateProductVM.File);
            _fileService.CheckSize(model.CreateProductVM.File, 250);
            var newFile = await _fileService.UploadAsync(model.CreateProductVM.File);
            Product product = new Product()
            {
                Name = model.CreateProductVM.Name,
                Delivery = model.CreateProductVM.Delivery,
                Description = model.CreateProductVM.Description,
                CategoryId = model.CreateProductVM.CategoryId,
                FilePath = newFile,
                CreatedAt = model.CreateProductVM.CreatedAt

            };
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            var product = await _context.Products.FindAsync(id);
            var category = await _context.Categories.Include(a=>a.Products).ToListAsync();
            if (product == null) return NotFound();
            UpdateProductVM vm = new UpdateProductVM()
            {
                Id =product.Id,
                Name = product.Name,
                Delivery = product.Delivery,
                Description = product.Description,
                CategoryId = product.CategoryId,
                Price = product.Price,
                FilePath = product.FilePath,
                CreatedAt = product.CreatedAt
            };

            ProductUpdateCategoryVM updateCategoryProductVm = new() {
                
                Categories=category,
                UpdateProductVm = vm
            
            } ;

            
            
            
            return View(updateCategoryProductVm);


        }
        [HttpPost]
        public async Task<IActionResult> Update( ProductUpdateCategoryVM model)
        {
            var product = await _context.Products.FindAsync(model.UpdateProductVm.Id);
     
            var path = Path.Combine(Directory.GetCurrentDirectory(), "\\assets\\images\\", product.FilePath);
            _fileService.Delete(path);
            if (model.UpdateProductVm.File != null)
            {

                _fileService.IsImage(model.UpdateProductVm.File);
                _fileService.CheckSize(model.UpdateProductVm.File, 250);
                var newFile = await _fileService.UploadAsync(model.UpdateProductVm.File);
                product.FilePath = newFile;
            }

            product.Name = model.UpdateProductVm.Name;
            product.Price = model.UpdateProductVm.Price;
            product.CategoryId = model.UpdateProductVm.CategoryId;
            product.Delivery = model.UpdateProductVm.Delivery;
            product.Description = model.UpdateProductVm.Description;
            product.CreatedAt = model.UpdateProductVm.CreatedAt;
           


            _context.Products.Update(product);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            var path = Path.Combine(Directory.GetCurrentDirectory(), "\\assets\\images\\", product.FilePath);
            _fileService.Delete(path);
            _context.Products.Remove(product);
            return RedirectToAction(nameof(Index));
        }
    }
}
