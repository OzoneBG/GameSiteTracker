namespace GST.Web.Infrastructure
{
    using GST.Data;
    using GST.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using System.Collections.Generic;
    using System.IO;

    public class SeedData
    {
        private readonly GSTDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SeedData(GSTDbContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Seed()
        {
            CreateRolesAndSuperUser();
        }

        public async void CreateRolesAndSuperUser()
        {
            bool exists = await _roleManager.RoleExistsAsync("Moderator");

            if (!exists)
            {
                var role = new IdentityRole();
                role.Name = "Moderator";
                await _roleManager.CreateAsync(role);
            }

            exists = await _roleManager.RoleExistsAsync("Administrator");

            if (!exists)
            {
                var role = new IdentityRole();
                role.Name = "Administrator";
                await _roleManager.CreateAsync(role);

                var adminUser = new User();

                //TO DO:
                //Get path to project.json programatically
                //else it won't work
                string envPath = File.ReadAllText("envPath.txt");

                var config = new ConfigurationBuilder()
                    .SetBasePath(envPath)
                    .AddJsonFile("appsettings.json")
                    .Build();

                string username, email, pwd;

                username = config["username"];
                email = config["email"];
                pwd = config["password"];

                File.Delete("envPath.txt");

                adminUser.UserName = username;
                adminUser.Email = email;
                string password = pwd;

                var userCreationResult = await _userManager.CreateAsync(adminUser, password);

                if (userCreationResult.Succeeded)
                {
                    List<string> roles = new List<string>();
                    roles.Add("Administrator");
                    roles.Add("Moderator");

                    var result = await _userManager.AddToRolesAsync(adminUser, roles);
                }
            }
        }
    }
}
