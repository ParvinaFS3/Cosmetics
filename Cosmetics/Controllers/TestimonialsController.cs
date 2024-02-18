using Cosmetics.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cosmetics.Controllers
{
    public class TestimonialsController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public TestimonialsController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IActionResult> Index()
        {
            var makeupFooter = await _appDbContext.MakeupFooter.ToListAsync();
            return View(makeupFooter);
        }
    }
}
