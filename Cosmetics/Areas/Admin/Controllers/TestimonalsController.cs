using Cosmetics.Areas.Admin.ViewModels.Testimonal;
using Cosmetics.DAL;
using Cosmetics.Helper;
using Cosmetics.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cosmetics.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class TestimonalsController : Controller
    {
        readonly private AppDbContext _context;

        readonly private IFileService _fileService;

        public TestimonalsController(AppDbContext context, IFileService fileService)
        {
            _context = context;
            _fileService = fileService;
        }
        public async Task<IActionResult> Index()
        {
            var testimonal = await _context.Testimonals.Include(a=>a.Backgrounds).ToListAsync();


            return View(testimonal);
        }
        public async Task<IActionResult> Detail(int id)
        {
            var testimonal = await _context.Testimonals.FindAsync(id);

            if (testimonal == null) return NotFound();


            return View(testimonal);
        }

        public async Task<IActionResult> Create()
        => View();
        [HttpPost]
        public async Task<IActionResult> Create(CreateTestimonalVM model)
        {
            if (model.File == null)
            {
                ModelState.AddModelError("File", "Profile image is required!");
                return View(model);
            }

            _fileService.IsImage(model.File);
            _fileService.CheckSize(model.File, 250);
            var profileImage = await _fileService.UploadAsync(model.File);

            if (model.Backgrounds == null || model.Backgrounds.Count == 0)
            {
                ModelState.AddModelError("Backgrounds", "At least one background photo is required!");
                return View(model);
            }

            var testimonial = new Testimonals
            {
              
                FilePath = _context.TestimonalBackground.FirstOrDefault().FilePath
               
            };

            _context.Testimonals.Add(testimonial);
            await _context.SaveChangesAsync();

            foreach (var background in model.Backgrounds)
            {
                _fileService.CheckSize(background, 250);
                _fileService.IsImage(background);

                var backgroundFile = await _fileService.UploadAsync(background);

                var testimonialBackground = new TestimonalBackground
                {
                    TestimonalsId = testimonial.Id,
                    FilePath = backgroundFile
                };

                _context.TestimonalBackground.Add(testimonialBackground);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }




        public async Task<IActionResult> Update(int id)
        {
            var testimonial = await _context.Testimonals.FindAsync(id);

            if (testimonial == null) return NotFound();

            UpdateTestimonalVM vm = new UpdateTestimonalVM()
            {
                Id = testimonial.Id,
         
                FilePath = testimonial.FilePath



            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateTestimonalVM model)
        {
            var testimonial = await _context.Testimonals.FindAsync(model.Id);

            if (testimonial == null) return NotFound();

         
            var path2 = Path.Combine(Directory.GetCurrentDirectory(), "\\assets\\images\\", testimonial.FilePath);

         

            if (model.Backgrounds != null)
            {


                foreach (var background in model.Backgrounds)
                {
                    _fileService.CheckSize(background, 250);
                    _fileService.IsImage(background);

                    var backgroundFile = await _fileService.UploadAsync(background);

                    var testimonialBackground = new TestimonalBackground
                    {
                        TestimonalsId = testimonial.Id,
                        FilePath = backgroundFile
                    };

                    _context.TestimonalBackground.Update(testimonialBackground);
                }

                await _context.SaveChangesAsync();
                testimonial.FilePath = _context.TestimonalBackground.FirstOrDefault().FilePath;

            }

            return RedirectToAction(nameof(Index));

        }
    }
}