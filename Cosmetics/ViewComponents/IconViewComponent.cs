using Cosmetics.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cosmetics.ViewComponents
{
    public class IconViewComponent: ViewComponent
    {
        private readonly AppDbContext _appDbContext;

        public IconViewComponent(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            var icons = await _appDbContext.Icons.ToListAsync();
            return View(icons);



        }
    }
}
