using Cosmetics.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cosmetics.ViewComponents
{
    public class ServiceViewComponent: ViewComponent
    {
        private readonly AppDbContext _appDbContext;

        public ServiceViewComponent(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async  Task <IViewComponentResult> InvokeAsync() 
        {
        
          var services = await _appDbContext.Services.ToListAsync();
           return View(services);


        
       }

    }
}
