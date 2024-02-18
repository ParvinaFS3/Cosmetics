using Cosmetics.Areas.Admin.ViewModels.FeaturedProduct;
using Cosmetics.Areas.Admin.ViewModels.Header;
using Cosmetics.Areas.Admin.ViewModels.Message;
using Cosmetics.Areas.Admin.ViewModels.Testimonal;
using Cosmetics.DAL;
using Cosmetics.Helper;
using Cosmetics.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cosmetics.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class MessageController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IFileService _fileService;

        public MessageController(AppDbContext appDbContext, IFileService fileService)
        {
            _appDbContext = appDbContext;
            _fileService = fileService;
        }
        public async Task<IActionResult> Index()
        {
            List<Message> messages = await _appDbContext.Messages.ToListAsync();
            return View(messages);
        }
        public async Task<IActionResult> Details(int id)
        {
            var message = await _appDbContext.Messages.FindAsync(id);
            if (message == null) return NotFound();
            return View(message);
        }
        public async Task<IActionResult> Create()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateMessageVM model)
        {

            Message message = new Message()
            {
                Name = model.Name,
                SurName = model.SurName,
                Email = model.Email,
                MessageInfo = model.MessageInfo
            };

            await _appDbContext.AddAsync(message);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Update(int id)
        {
            var message = await _appDbContext.Messages.FindAsync(id);

            if (message == null) return NotFound();

            UpdateMessageVM updateMessageVM = new UpdateMessageVM()
            {
                Id = message.Id,

                Name = message.Name, 
                SurName = message.SurName,
                Email = message.Email,
                MessageInfo = message.MessageInfo



            };
            return View(updateMessageVM);
        }


        [HttpPost]
        public async Task<IActionResult> Update(UpdateMessageVM model)
        {
            if (!ModelState.IsValid) return View(model);

            var message = await _appDbContext.Messages.FindAsync(model.Id);
            if (message == null) return BadRequest();
            message.Name = model.Name;
            message.SurName = model.SurName;
            message.Email = model.Email;  
            message.MessageInfo = model.MessageInfo;    

            _appDbContext.Messages.Update(message);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }




        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var message = await _appDbContext.Messages.FindAsync(id);
         
            _appDbContext.Messages.Remove(message);
            return RedirectToAction(nameof(Index));
        }
    }

}
