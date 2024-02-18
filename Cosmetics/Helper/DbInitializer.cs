using Cosmetics.Models.Constants;
using Cosmetics.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace Cosmetics.Helper
{
    public static class DbInitializer
    {
        public async static Task SeedAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {

            foreach (var role in Enum.GetValues(typeof(RoleModel)))
            {

                if (!await roleManager.RoleExistsAsync(role.ToString()))
                {
                    await roleManager.CreateAsync(new IdentityRole
                    {

                        Name = role.ToString(),

                    });



                }

            }
            AppUser admin = new AppUser()
            {
                NameSurName = "admin adminli",
                UserName = "admin",
                Email = "admin@inbox.ru"
            };

            if (await userManager.FindByNameAsync(admin.UserName) == null)
            {
                var result = await userManager.CreateAsync(admin, "Admin123*");

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        throw new Exception(error.Description);
                    }
                }

                await userManager.AddToRoleAsync(admin, RoleModel.SuperAdmin.ToString());
            }


        }
    }

}



