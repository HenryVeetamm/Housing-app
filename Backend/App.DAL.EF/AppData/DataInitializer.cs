using App.Domain;
using App.Domain.Identity;
using Microsoft.AspNetCore.Identity;

namespace App.DAL.EF.AppData;

public static class DataInitializer
{
    public static async void SeedAppData(AppDbContext ctx)
    {
        /*await SeedFooBars(ctx);*/
    }

    /*public static async Task SeedFooBars(AppDbContext ctx)
    {
        foreach (var foo in InitialData.Foos)
        {
            await ctx.FooBars.AddAsync(new FooBar()
            {
                Name = foo
            });

            await ctx.SaveChangesAsync();
        }
    }*/

    public static void SeedRoles(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
    {
        foreach (var roleName in InitialData.Roles)
        {
            var role = roleManager.FindByNameAsync(roleName).Result;
            if (role == null)
            {
                role = new AppRole()
                {
                    Name = roleName
                };

                var result = roleManager.CreateAsync(role).Result;
            }
        }

        foreach (var userInfo in InitialData.Users)
        {
            var user = new AppUser()
            {
                FirstName = userInfo.FirstName,
                LastName = userInfo.LastName,
                UserName = userInfo.email,
                Email = userInfo.email,
                EmailConfirmed = true
            };
            var result = userManager.CreateAsync(user, userInfo.password).Result;

            var userResult = userManager.AddToRoleAsync(user, userInfo.role).Result;
        }
    }
}