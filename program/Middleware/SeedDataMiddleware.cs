using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SchoolDiary.Data;
using SchoolDiary.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolDiary.Web.Middleware
{
    public class SeedDataMiddleware
    {
        private readonly RequestDelegate next;

        public SeedDataMiddleware(RequestDelegate next)
        {
            this.next = next;
        }


        public async Task InvokeAsync(HttpContext context
         , SchoolDbContext dbContext
         , UserManager<User> userManager,
         RoleManager<IdentityRole> roleManager)
        {

            if (!dbContext.Roles.Any())
            {
                await this.SeedRoles(userManager, roleManager);
            }

            await this.next(context);
        }

        public async Task SeedRoles(
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            var adminResult = await roleManager.CreateAsync(new IdentityRole("Admin"));
            var userResult = await roleManager.CreateAsync(new IdentityRole("Student"));
        }
    }
}
