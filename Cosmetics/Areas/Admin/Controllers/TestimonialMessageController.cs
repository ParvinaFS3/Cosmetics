using Cosmetics.Areas.Admin.ViewModels.Header;
using Cosmetics.Areas.Admin.ViewModels.TestimonialMessage;
using Cosmetics.DAL;
using Cosmetics.Helper;
using Cosmetics.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cosmetics.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class TestimonialMessageController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IFileService _fileService;

        public TestimonialMessageController(AppDbContext context, IFileService fileService)
        {
            _context = context;
            _fileService = fileService;
        }

        public async Task<IActionResult> Index()
        {
            List<TestimonialMessage> testimonialMessages = await _context.TestimonialMessages.ToListAsync();
            return View(testimonialMessages);
        }
        public async Task<IActionResult> Create()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateTestimonialMessageVM model)
        {
            _fileService.IsImage(model.formFile);
            _fileService.CheckSize(model.formFile, 250);
            var newFile = await _fileService.UploadAsync(model.formFile);
            TestimonialMessage testimonialMessage = new TestimonialMessage()
            {
                FullName= model.FullName,
                Message= model.Message,           
                FilePath = newFile
            };

            await _context.AddAsync(testimonialMessage);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Update(int id)
        {
            var testimonialMessage = await _context.TestimonialMessages.FindAsync(id);
            if (testimonialMessage == null) return NotFound();
            UpdateTestimonialMessageVM updateTestimonialMessageVM = new UpdateTestimonialMessageVM()
            {
                Id = testimonialMessage.Id,
                FullName = testimonialMessage.FullName,
                Message = testimonialMessage.Message,
               
                FilePath = testimonialMessage.FilePath,
              

            };

            return View(updateTestimonialMessageVM);

        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateTestimonialMessageVM model)
        {


            var testimonialMessage = await _context.TestimonialMessages.FindAsync(model.Id);
            if (testimonialMessage == null) return BadRequest();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "\\assets\\img\\", testimonialMessage.FilePath);
            _fileService.Delete(path);
            if (model.formFile != null)
            {

                _fileService.IsImage(model.formFile);
                _fileService.CheckSize(model.formFile, 250);
                var newFile = await _fileService.UploadAsync(model.formFile);
                testimonialMessage.FilePath = newFile;
            }


            testimonialMessage.FullName = model.FullName;
            testimonialMessage.Message = model.Message;       

            return RedirectToAction(nameof(Index));

        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var testimonialMessage = await _context.TestimonialMessages.FindAsync(id);
            var path = Path.Combine(Directory.GetCurrentDirectory(), "\\assets\\images\\", testimonialMessage.FilePath);
            _fileService.Delete(path);
            _context.TestimonialMessages.Remove(testimonialMessage);
            return RedirectToAction(nameof(Index));
        }




    }
}
