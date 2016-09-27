namespace GST.Web.Controllers
{
    using Common.Mapping;
    using Data.Services.Interfaces;
    using InputModels.PagesInputModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using System.Text;
    using ViewModels.PagesViewModel;

    public abstract class ModeratorController : Controller
    {
        protected readonly IPagesService pageService;

        public ModeratorController(IPagesService pageService)
        {
            this.pageService = pageService;
        }

        [Authorize(Roles = "Administrator, Moderator")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Administrator, Moderator")]
        [HttpGet]
        public IActionResult EditStaticPages(string pageName)
        {
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
                pageService.EditStaticPage(model.PageName, model.Content);

                return RedirectToAction("EditStaticPages", new { pageName = model.PageName });
            }
            else
            {
                var pageData = PopulateAdminPageViewModel(model.PageName);

                pageData.Content = "";

                ViewData["message"] = "Content cannot be empty!";

                return View(pageData);
            }
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
