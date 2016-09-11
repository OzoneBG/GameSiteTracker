namespace GST.Web.Controllers
{
    using GST.Data.Common.Repository;
    using GST.Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;

    public class HomeController : Controller
    {
        private readonly IDeletableEntityRepository<Video> videos;

        public HomeController(IDeletableEntityRepository<Video> videos)
        {
            this.videos = videos;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "This is some lorem ipsum information about the game.";

            return View();
        }

        public IActionResult Contact()
        {

            return View();
        }

        public IActionResult Progress()
        {

            return View();
        }

        public IActionResult Downloads()
        {

            return View();
        }

        public IActionResult Pictures()
        {

            return View();
        }

        public IActionResult Videos()
        {
            var vids = videos.All().ToList();

            return View(vids);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
