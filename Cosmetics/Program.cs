using Cosmetics.DAL;
using Cosmetics.Helper;
using Cosmetics.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
var connectionString = builder.Configuration.GetConnectionString("Default");

builder.Services.AddDbContext<AppDbContext>(
    x => x.UseSqlServer(connectionString)
    );
builder.Services.AddIdentity<AppUser , IdentityRole>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredUniqueChars = 1;
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.User.RequireUniqueEmail = true;  


}).AddEntityFrameworkStores<AppDbContext>();



builder.Services.AddSingleton<IFileService , FileService>();   
var app = builder.Build();
 
app.UseAuthentication();
app.UseAuthorization();



app.UseStaticFiles();
app.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
   );

app.MapDefaultControllerRoute();

 var ScopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
using (var scope = ScopeFactory.CreateScope()) 
{
    var roleManager = scope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetService<UserManager<AppUser>>();
   await  DbInitializer.SeedAsync(userManager, roleManager);
}


    app.Run();