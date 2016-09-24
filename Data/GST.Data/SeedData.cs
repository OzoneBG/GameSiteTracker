namespace GST.Data
{
    using Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using System.Collections.Generic;
    using System.IO;
    using Services.Interfaces;

    public class SeedData
    {
        private readonly GSTDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IPagesService pages;

        public SeedData(GSTDbContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IPagesService pages)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            this.pages = pages;
        }

        public void Seed()
        {
            CreateRolesAndSuperUser();
            CreateDefaultPages();
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
                string envPath = Directory.GetCurrentDirectory();

                var config = new ConfigurationBuilder()
                    .SetBasePath(envPath)
                    .AddJsonFile("appsettings.json")
                    .Build();

                string username, email, pwd;

                username = config["username"];
                email = config["email"];
                pwd = config["password"];

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

        public void CreateDefaultPages()
        {
            if (pages.ShouldCreateDefaultPages())
            {

                Page Progress = new Page
                {
                    Name = "Progress",
                    Content = "Some placeholder content for progress page.",
                };

                Page Downloads = new Page
                {
                    Name = "Downloads",
                    Content = "DOWNLOAD LINKUS EVERYWHEREEEEEEE"
                };

                Page About = new Page
                {
                    Name = "About",
                    Content = "Some very cool information about the game"
                };

                Page Contacts = new Page
                {
                    Name = "Contacts",
                    Content = "Some information to contact the developer"
                };

                List<Page> pageList = new List<Page>();

                pageList.Add(Progress);
                pageList.Add(Progress);
                pageList.Add(Downloads);
                pageList.Add(About);
                pageList.Add(Contacts);

                pages.AddDefaultPages(pageList);
            }
        }
    }
}
