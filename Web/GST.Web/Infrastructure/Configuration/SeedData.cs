namespace GST.Web.Infrastructure
{
    using Configuration;
    using GST.Data;
    using GST.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;

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
            bool exists = await _roleManager.RoleExistsAsync("Admin");

            if (!exists)
            {
                var role = new IdentityRole();
                role.Name = "Administrator";
                await _roleManager.CreateAsync(role);

                var adminUser = new User();

                //TO DO:
                //Load these from config file

                var config = new ConfigurationBuilder().AddJsonFile(@"F:\Projects\GameSiteTracker\Web\GST.Web\project.json").Build();
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
                    var result = await _userManager.AddToRoleAsync(adminUser, "Administrator");
                }
            }

            exists = await _roleManager.RoleExistsAsync("Moderator");

            if (!exists)
            {
                var role = new IdentityRole();
                role.Name = "Moderator";
                await _roleManager.CreateAsync(role);
            }
        }
    }
}
