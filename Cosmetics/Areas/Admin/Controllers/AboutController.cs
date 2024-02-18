using Cosmetics.Areas.Admin.ViewModels.About;
using Cosmetics.Areas.Admin.ViewModels.Product;
using Cosmetics.Areas.Admin.Views.Product;
using Cosmetics.DAL;
using Cosmetics.Helper;
using Cosmetics.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cosmetics.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class AboutController : Controller
    {
        readonly private AppDbContext _appDbContext;
        readonly private IFileService _fileService;
        public AboutController(AppDbContext appDbContext, IFileService fileService)
        {

            _appDbContext = appDbContext;
            _fileService = fileService;
        }

        public async Task<IActionResult> Index()
        {
            var about = await _appDbContext.About
                                        .Include(a => a.Photos)

                                        .Include(f => f.FeaturedProducts).Select(a => new About() { 
                                          Id=a.Id,
                                          Title=a.Title,
                                          FilePath=a.FilePath,
                                          Description=a.Description,
                                          FeaturedProducts=a.FeaturedProducts,
                                          Photos=a.Photos   

                                        
                                        
                                        }).ToListAsync();



            return View(about);

        }
        public async Task<IActionResult> Details(int id)
        {
            var about = await _appDbContext.About.FindAsync(id);
            if (about == null) return NotFound();
            return View(about);
        }

        public async Task<IActionResult> Create()
        => View();

      
        [HttpPost]
        public async Task<IActionResult> Create(CreateAboutVM model)
        {
            if (model.Photos == null || model.Photos.Count == 0)
            {
                ModelState.AddModelError("Photos", "En az bir fotoğraf gereklidir!");
                return View(model);
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }


            About about = new About()
            {
                Description = model.Description,
                Title = model.Title,
                Content=model.Content,
                FilePath = _appDbContext.AboutPhotos.FirstOrDefault()?.FilePath
            };
            await _appDbContext.About.AddAsync(about);
            await _appDbContext.SaveChangesAsync();

            foreach (var img in model.Photos)
            {
                if (!_fileService.IsImage(img))
                {
                    ModelState.AddModelError("Photos", "Geçersiz resim formatı!");
                    return View(model);
                }

                _fileService.CheckSize(img, 250);
                var newFile = await _fileService.UploadAsync(img);
                int i = 0;
                AboutPhoto aboutPhoto = new AboutPhoto()
                {
                    AboutId = about.Id,
                    FilePath = newFile,
                    Order = i++
                };

                await _appDbContext.AboutPhotos.AddAsync(aboutPhoto);
            }

            await _appDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Update(int id)
        {
            var about = await _appDbContext.About.FindAsync(id);
            if (about == null) return NotFound();

            UpdateAboutVM vm = new UpdateAboutVM()
            {
                Id=about.Id,
                Description= about.Description,
                Content=about.Content,
                FilePath=about.FilePath,
                Title=about.Title,  
               

            };
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateAboutVM model)
        {
            if (!ModelState.IsValid) return View(model);
            var about = await _appDbContext.About.FindAsync(model.Id);

            if (about == null) return NotFound();

            var path = Path.Combine(Directory.GetCurrentDirectory(), "\\wwwroot\\assets\\images", about.FilePath);
             _fileService.Delete(path);
            if (model.Photos != null )
            {
                
                foreach (var img in model.Photos)
                {
                   
                    _fileService.IsImage(img);
                    _fileService.CheckSize(img, 250);
                    var newFile = await _fileService.UploadAsync(img);

                    _appDbContext.AboutPhotos.Update(new AboutPhoto()
                    {
                        AboutId = about.Id,
                        FilePath = newFile
                    });

                }
               
                await _appDbContext.SaveChangesAsync();

                about.FilePath =  _appDbContext.AboutPhotos.FirstOrDefault()?.FilePath;
            }

            about.Description=model.Description;
            about.Title = model.Title;
            about.Content = model.Content;

             _appDbContext.About.Update(about);
            await _appDbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }



        [HttpPost]
        public async Task<IActionResult> Delete( string id)
        {
            var about = await _appDbContext.About.FindAsync(id);

            if (about == null) return NotFound();

            var path = Path.Combine(Directory.GetCurrentDirectory(), "\\wwwroot\\assets\\images", about.FilePath);
             _fileService.Delete(path);
            return RedirectToAction(nameof(Index));
        }
    }
}
