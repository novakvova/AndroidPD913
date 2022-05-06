using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NewMail.Data.Constants;
using NewMail.Data.Entities.Identity;

namespace NewMail.Data
{
    public static class SeederDB
    {
        public static void SeedData(this IApplicationBuilder app)
        {
            using(var scope = 
                app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<AppEFContext>>();
                try
                {
                    logger.LogInformation("Migrate DB Databases");
                    var context = scope.ServiceProvider.GetRequiredService<AppEFContext>();
                    context.Database.Migrate();
                    logger.LogInformation("Seeding Web And Localization Databases");
                    InitRoleAndUsers(scope);
                }
                catch (Exception ex)
                {
                    logger.LogError("Problem seed database " + ex.Message);

                }
            }
        }

        private static void InitRoleAndUsers(IServiceScope scope)
        {
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
            if(!roleManager.Roles.Any())
            {
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<RoleManager<AppRole>>>();
                var result = roleManager.CreateAsync(new AppRole
                {
                    Name = Roles.Admin
                }).Result;
                if (result.Succeeded)
                    logger.LogWarning($"Create role {Roles.Admin}");
                else
                    logger.LogError($"Problem crate role {Roles.Admin}");
                result = roleManager.CreateAsync(new AppRole
                {
                    Name = Roles.User
                }).Result;
            }
            if (!userManager.Users.Any())
            {
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<UserManager<AppUser>>>();
                string email = "admin@gmail.com";
                var user = new AppUser
                {
                    Email = email,
                    UserName = email,
                    FirstName = "Петро",
                    SecondName = "Шпрот",
                    PhoneNumber = "+38(098)232 34 22",
                    Photo = "1.jpg"
                };
                var result = userManager.CreateAsync(user, "12345").Result;
                if (result.Succeeded)
                {
                    logger.LogWarning("Create user " + user.UserName);
                    result = userManager.AddToRoleAsync(user, Roles.Admin).Result;
                }
                else
                {
                    logger.LogError("Faild create user " + user.UserName);
                }
            }
        }
    }
}
