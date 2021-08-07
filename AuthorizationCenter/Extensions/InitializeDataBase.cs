using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Models;

namespace AuthorizationCenter.Extensions
{
    /// <summary>
    ///     初始化数据库
    /// </summary>
    public static class InitializeDataBase
    {
        /// <summary>
        ///     初始化种子数据
        /// </summary>
        /// <param name="app"></param>
        public static async void InitializeSeedData(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>()?.CreateScope();
            if (serviceScope == null) return;
            var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            await EnsureSeedData(userManager, roleManager);
        }

        /// <summary>
        ///     确认初始化数据
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="roleManager"></param>
        /// <returns></returns>
        public static async Task EnsureSeedData(UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager)
        {
            Console.WriteLine("开始初始化数据库");
            var user = await userManager.FindByNameAsync("fqydhk");
            if (!await roleManager.RoleExistsAsync("Admin"))
                await roleManager.CreateAsync(new ApplicationRole {Name = "admin"});
            if (!await roleManager.RoleExistsAsync("Guest"))
                await roleManager.CreateAsync(new ApplicationRole {Name = "guest"});
            if (user == null)
            {
                user = new ApplicationUser
                {
                    Id = Guid.NewGuid(),
                    NickName = "顺其自然",
                    UserName = "fqydhk",
                    Email = "zhouf@hwyuan.com",
                    IsDelete = false
                };
                await userManager.CreateAsync(user, "558428hK");
                await userManager.AddToRoleAsync(user, "Admin");
                await userManager.AddToRoleAsync(user, "Guest");
                Console.WriteLine("初始化数据库完成");
            }
        }
    }
}