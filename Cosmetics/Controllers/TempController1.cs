using Cosmetics.Models.Constants;
using Cosmetics.Models.Identity;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Cosmetics.Controllers
{
    public class TempController1 : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public TempController1(
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> rolemanager
            )
        {
             _userManager= userManager;
            _roleManager = rolemanager;

        }
        public async Task <IActionResult> Test()
        {   
            foreach( var role in Enum.GetValues(typeof(RoleModel)))
            {
               await _roleManager.CreateAsync(new IdentityRole
                {


                    Name= role.ToString(),

                });
               
            }
            AppUser admin = new()
            {
                NameSurName = "admin adminli",
                UserName = "admin",
                Email = "admin@inbox.ru"

            };

            var result= await _userManager.CreateAsync(admin, "Admin123*");
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors) 
                {
                   
                    throw new Exception(error.Description);
                              
                }  
            }
           await _userManager.AddToRoleAsync(admin, RoleModel.SuperAdmin.ToString());

            return Ok();
        }
    }
}
