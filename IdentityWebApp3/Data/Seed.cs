using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityWebApp3.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityWebApp3.Data
{
    public static class Seed
    {
        public static async Task CreateRoles(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            string[] roleNames = {"Admin", "Manager", "Member"};
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var exists = await roleManager.RoleExistsAsync(roleName);
                if (!exists)
                {
                    roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            var email = configuration.GetSection("AdminUser")["UserEmail"];
            var password = configuration.GetSection("AdminUser")["UserPassword"];
            var admin = new ApplicationUser
            {
                UserName = email,
                Email = email
            };

            var existingAdmin = await userManager.FindByEmailAsync(email);
            if (existingAdmin == null)
            {
                var creationResult = await userManager.CreateAsync(admin, password);
                if (creationResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
            }
        }
    }
}
