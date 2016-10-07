namespace GST.Web.Controllers
{
    using Data.Models;
    using Data.Services.Interfaces;
    using Microsoft.AspNetCore.Identity;

    public class AdministrationAreaController : AdministratorController
    {
        public AdministrationAreaController(IPagesService pageService,
            IUsersService usersService,
            ILogService logsService,
            IVideosService videosService,
            IPicturesService picturesService,
            IPostsService postsService,
            UserManager<User> userManger) 
            : base(pageService, usersService, logsService, videosService, picturesService, postsService, userManger)
        {
        }
    }
}
