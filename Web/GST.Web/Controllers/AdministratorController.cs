namespace GST.Web.Controllers
{
    using Common.Extensions;
    using Common.Mapping;
    using Data.Models;
    using Data.Services.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using ViewModels.LogsViewModels;

    public abstract class AdministratorController : ModeratorController
    {
        public AdministratorController(IPagesService pageService,
            IUsersService usersService,
            ILogService logsService,
            IVideosService videosService,
            IPicturesService picturesService,
            IPostsService postsService,
            UserManager<User> userManger) 
            : base(pageService, usersService, logsService, videosService, picturesService, postsService, userManger)
        {
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult RemoveUser(string guid)
        {
            usersService.DeleteUser(guid);

            string content = string.Format("{0} requested deletion of user with id: {1} and username: {2}.", GetCurrentUserName(User.GetUserId()), guid, GetCurrentUserName(guid));
            logsService.AddNewLog("Delete", content);

            return RedirectToAction("AllUsers");
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult ListLogs(int? p)
        {
            int page = PageChecks(p, "ListLogs");

            var allLogs = logsService.GetAllLogs();

            ViewBag.TotalLinksToDisplay = GetLinksCountFor(allLogs.Count());
            ViewBag.CurrentPage = page;

            int toSkip = GetPaginationDataToSkip(page);

            var logsList = allLogs.Skip(toSkip).Take(MaxLogsPerPage).To<LogsViewModel>().ToList();

            return View(logsList);
        }
    }
}
