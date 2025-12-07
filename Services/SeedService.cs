using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Identity;
using GerenciadorEventos.Enums;
using GerenciadorEventos.Infrastructure.Databases;
using GerenciadorEventos.Models;
using Microsoft.AspNetCore.Identity;

namespace GerenciadorEventos.Services
{
    public class SeedService
    {
        public static void SeedDatabase(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<SeedService>>();

            try
            {
                //Verifica se o banco de dados está pronto
                logger.LogInformation("Ensuring the database is created");
                context.Database.EnsureCreated();

                //Add Roles
                logger.LogInformation("Seeding Roles");
                AddRole(roleManager, Role.Client.ToString());
                AddRole(roleManager, Role.Organizer.ToString());

                logger.LogInformation("Seeding Admin User: ");
                var adminEmail = "pirao@gmail.com";

                if (userManager.FindByEmailAsync(adminEmail).GetAwaiter().GetResult() == null)
                {
                    var adminUser = new User
                    {
                        UserName = "CodeHub",
                        NormalizedUserName = "CodeHub".ToUpper(),
                        Email = adminEmail,
                        NormalizedEmail = adminEmail.ToUpper(),
                        EmailConfirmed = true,
                        SecurityStamp = Guid.NewGuid().ToString()
                    };

                    var result = userManager.CreateAsync(adminUser, "pk000000").GetAwaiter().GetResult();

                    if (result.Succeeded)
                    {
                        logger.LogInformation("Assigning Admin role to the admin user.");
                        userManager.AddToRoleAsync(adminUser, Role.Organizer.ToString()).GetAwaiter().GetResult();
                    }
                    else
                    {
                        logger.LogError("Failed to create admin user: {Errors}", string.Join(",", result.Errors.Select(e => e.Description)));
                    }
                }

            }
            catch(Exception e)
            {
                logger.LogError(e, "An error ocurred while seeding the database");
            }
        }
        
        private static void AddRole(RoleManager<IdentityRole> roleManager, string roleName)
        {
            if(!roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
            {
                var result = roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
                if (!result.Succeeded)
                    throw new Exception($"Failed to create role '{roleName}': {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }
        }
    }
}