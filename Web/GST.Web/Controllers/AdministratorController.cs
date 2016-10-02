namespace GST.Web.Controllers
{
    using Data.Models;
    using Data.Services.Interfaces;
    using Microsoft.AspNetCore.Identity;

    public abstract class AdministratorController : ModeratorController
    {
        public AdministratorController(IPagesService pageService, IUsersService usersService, ILogService logsService, UserManager<User> userManger) : base(pageService, usersService, logsService, userManger)
        {
        }
    }
}
