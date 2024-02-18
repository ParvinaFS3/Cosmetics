using Cosmetics.Models.Constants;
using Microsoft.AspNetCore.Identity;

namespace Cosmetics.Models.Identity
{
    public class AppRole:IdentityRole
    {
      public  RoleModel RoleModel { get; set; }
    }
}
