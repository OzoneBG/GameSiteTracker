namespace GST.Web.Controllers
{
    using Common;
    using Common.Extensions;
    using Common.Mapping;
    using Data.Models;
    using Data.Services.Interfaces;
    using InputModels.PagesInputModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using System.Threading.Tasks;
    using ViewModels.AccountViewModels;
    using ViewModels.PagesViewModel;
    using ViewModels.LogsViewModels;

    public abstract class ModeratorController : BaseController
    {
        //Services
        protected readonly IPagesService pageService;
        protected readonly IUsersService usersService;
        protected readonly ILogService logsService;

        //Managers
        protected readonly UserManager<User> userManager;

        public ModeratorController(IPagesService pageService, IUsersService usersService, ILogService logsService, UserManager<User> userManager)
        {
            this.pageService = pageService;
            this.usersService = usersService;
            this.logsService = logsService;

            this.userManager = userManager;
            
        }

        public IActionResult ListLogs(int? p)
        {
            int page = PageChecks(p, "ListLogs");

            var allLogs = logsService.GetAllLogs();

            ViewBag.TotalLinksToDisplay = GetLinksCountFor(allLogs.Count());
            ViewBag.CurrentPage = page;

            int toSkip = GetPaginationDataToSkip(page);

            var logsList = allLogs.Skip(toSkip).Take(MaxMediaPerPage).To<LogsViewModel>().ToList();

            return View(logsList);
        }

        [Authorize(Roles = "Administrator, Moderator")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Administrator, Moderator")]
        public IActionResult AllUsers()
        {
            var allUsers = usersService.GetAllUsers().To<ViewUsersViewModel>().ToList();

            return View(allUsers);
        }

        [Authorize(Roles = "Administrator, Moderator")]
        public IActionResult RemoveUser(string guid)
        {
            usersService.DeleteUser(guid);

            string content = string.Format("{0} requested deletion of user with id: {1} and username: {2}.", GetCurrentUserName(User.GetUserId()), guid, GetCurrentUserName(guid));
            logsService.AddNewLog("Delete", content);

            return RedirectToAction("AllUsers");
        }

        [Authorize(Roles = "Administrator, Moderator")]
        [HttpGet]
        public IActionResult EditStaticPages(string pageName)
        {
            if (string.IsNullOrWhiteSpace(pageName))
            {
                pageName = pageService.GetFirstPageName();
            }

            var pageData = PopulateAdminPageViewModel(pageName);

            return View(pageData);
        }

        [Authorize(Roles = "Administrator, Moderator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditStaticPages(EditPageInputModel model)
        {
            if (ModelState.IsValid)
            {
                pageService.EditStaticPage(model.OldName, model.Name, model.Content);

                string content = string.Format("{0} modified contents of pagename: {1} / {2}.", GetCurrentUserName(User.GetUserId()), model.OldName, model.Name);
                logsService.AddNewLog("Modify", content);

                return RedirectToAction("EditStaticPages", new { pageName = model.Name });
            }
            else
            {
                var pageData = PopulateAdminPageViewModel(model.Name);

                pageData.Content = "";

                ViewData["message"] = "Content cannot be empty!";

                return View(pageData);
            }
        }

        [Authorize(Roles = "Administrator, Moderator")]
        [HttpGet]
        public IActionResult NewPage()
        {
            return View();
        }

        [Authorize(Roles = "Administrator, Moderator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult NewPage(NewPageInputModel model)
        {
            if (ModelState.IsValid)
            {
                var user = GetCurrentUser(User.GetUserId()).Result;

                pageService.CreateNewPage(model.Name, model.Content, user);

                //Log
                //[UserName] created new page: {0}
                string content = string.Format("{0} created new page: {1}", GetCurrentUserName(User.GetUserId()), model.Name);
                logsService.AddNewLog("Create", content);

                return RedirectToAction("EditStaticPages", new { pageName = model.Name });
            }
            else
            {
                return View();
            }
        }

        public IActionResult DeletePage(int pageId)
        {
            string pageName = pageService.GetPageNameById(pageId);

            pageService.DeletePage(pageId);

            string content = string.Format("{0} requested deletion of page: {1}", GetCurrentUserName(User.GetUserId()), pageName);
            logsService.AddNewLog("Delete", content);

            return RedirectToAction("EditStaticPages");
        }

        #region Helpers
        private AdministrationPageViewModel PopulateAdminPageViewModel(string pageName)
        {
            var pageData = pageService.GetPageFor(pageName).To<AdministrationPageViewModel>().FirstOrDefault();

            var pageNames = pageService.GetAllPageNames();

            pageData.PageNames = pageNames;

            return pageData;
        }

        private Task<User> GetCurrentUser(string id) => userManager.FindByIdAsync(id);

        private string GetCurrentUserName(string id)
        {
            return GetCurrentUser(id).Result.UserName;
        }
        #endregion
    }
}
