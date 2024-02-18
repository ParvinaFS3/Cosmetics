using Cosmetics.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cosmetics.ViewComponents
{
    public class MakeupFooterViewComponent:ViewComponent
    {
        private readonly AppDbContext _appDbContext;

        public MakeupFooterViewComponent(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            var makeupFooter = await _appDbContext.MakeupFooter.ToListAsync();
            return View(makeupFooter);



        }
    }
}
