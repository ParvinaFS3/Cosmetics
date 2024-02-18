using Cosmetics.Areas.Admin.ViewModels.Message;
using Cosmetics.DAL;
using Cosmetics.Models;
using Cosmetics.ViewModels.Contact;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Cosmetics.Controllers
{
    public class ContactController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public ContactController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<IActionResult> Index()
        {
            var header = await _appDbContext.Headers.Select(a=> new Header() { Id=a.Id , Title=a.Title , OrderHeader=a.OrderHeader, FilePath=a.FilePath }).FirstOrDefaultAsync();
            var icons = await _appDbContext.Icons.ToListAsync();

            ContactIndexVM vm = new ContactIndexVM { 
             Header=header

            };

            return View(vm); 
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateMessageVM model)
        {
            Message message = new Message() { 
            Email=model.Email,
            MessageInfo=model.MessageInfo,
            Name=model.Name,
            SurName=model.SurName
            
            
            };

            await _appDbContext.Messages.AddAsync(message);
            await _appDbContext.SaveChangesAsync();


            return StatusCode((int)HttpStatusCode.Created);

        }
    }
}
