namespace GST.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public abstract class ModeratorController : Controller
    {
        [Authorize(Roles = "Adminstrator, Moderator")]
        public IActionResult Index()
        {
            //Display here ze menu list hehe

            return View();
        }

        [Authorize(Roles = "Adminstrator, Moderator")]
        public IActionResult ViewUserOptions()
        {
            return View();
        }

        [Authorize(Roles = "Adminstrator, Moderator")]
        public IActionResult ViewPicturesOptions()
        {
            return View();
        }

        [Authorize(Roles = "Adminstrator, Moderator")]
        public IActionResult ViewVideosOptions()
        {
            return View();
        }

        [Authorize(Roles = "Adminstrator, Moderator")]
        public IActionResult ViewStaticPagesOptions()
        {
            return View();
        }
    }
}
