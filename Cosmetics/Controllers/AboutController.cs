using Cosmetics.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cosmetics.Controllers
{
    public class AboutController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public AboutController(AppDbContext appDbContext)
        {

            _appDbContext = appDbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

    }
}
