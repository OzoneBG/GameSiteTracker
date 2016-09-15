namespace GST.Web.Infrastructure
{
    using GST.Data;
    using GST.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

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

        public async void Seed()
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
                adminUser.UserName = "Kyojin96";
                adminUser.Email = "muzunov@hotmail.com";
                string password = "123321";
            
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
