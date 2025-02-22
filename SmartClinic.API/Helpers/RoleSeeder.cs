using Microsoft.AspNetCore.Identity;
using SmartClinic.Domain.Entities;
using SmartClinic.Domain.Enum;
namespace SmartClinic.API.Helpers
{
    public class RoleSeeder
    {
        public static void SeedRoles(RoleManager<IdentityRole<Guid>> roleManager)
        {
            //seed role if not exist
            if(!roleManager.RoleExistsAsync(UserRole.Admin.ToString()).GetAwaiter().GetResult())
                roleManager.CreateAsync(new IdentityRole<Guid>(UserRole.Admin.ToString())).GetAwaiter().GetResult();

            if (!roleManager.RoleExistsAsync(UserRole.Doctor.ToString()).GetAwaiter().GetResult())
                roleManager.CreateAsync(new IdentityRole<Guid>(UserRole.Doctor.ToString())).GetAwaiter().GetResult();

            if (!roleManager.RoleExistsAsync(UserRole.Patient.ToString()).GetAwaiter().GetResult())
                roleManager.CreateAsync(new IdentityRole<Guid>(UserRole.Patient.ToString())).GetAwaiter().GetResult();
        }
    }
}
