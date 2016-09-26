namespace GST.Web.Controllers
{
    using Common.Mapping;
    using Data.Services.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
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
        public IActionResult ViewStaticPages(string pageName)
        {
            var pageData = pageService.GetPageFor(pageName).To<AdministrationPageViewModel>().FirstOrDefault();

            var pageNames = pageService.GetAllPageNames();

            pageData.PageNames = pageNames;

            return View(pageData);
        }

    }
}
