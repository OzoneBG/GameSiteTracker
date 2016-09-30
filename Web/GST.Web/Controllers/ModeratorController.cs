namespace GST.Web.Controllers
{
    using Common.Mapping;
    using Data.Services.Interfaces;
    using InputModels.PagesInputModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using ViewModels.AccountViewModels;
    using ViewModels.PagesViewModel;

    public abstract class ModeratorController : Controller
    {
        protected readonly IPagesService pageService;
        protected readonly IUsersService usersService;

        public ModeratorController(IPagesService pageService, IUsersService usersService)
        {
            this.pageService = pageService;
            this.usersService = usersService;
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
                pageService.CreateNewPage(model.Name, model.Content);

                return RedirectToAction("EditStaticPages", new { pageName = model.Name });
            }
            else
            {
                return View();
            }
        }

        public IActionResult DeletePage(int pageId)
        {
            pageService.DeletePage(pageId);

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
        #endregion
    }
}
