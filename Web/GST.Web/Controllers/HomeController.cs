namespace GST.Web.Controllers
{
    using GST.Data.Common.Repository;
    using GST.Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;

    public class HomeController : Controller
    {
        private readonly IDeletableEntityRepository<Video> videos;
        private readonly IDeletableEntityRepository<Page> pages;
        private readonly IDeletableEntityRepository<Picture> pictures;

        public HomeController(IDeletableEntityRepository<Video> videos, IDeletableEntityRepository<Page> pages, IDeletableEntityRepository<Picture> pictures)
        {
            this.videos = videos;
            this.pages = pages;
            this.pictures = pictures;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Videos()
        {
            var videosList = videos.All().ToList();

            return View(videosList);
        }

        public IActionResult Pictures()
        {
            var images = pictures.All().ToList();

            return View(images);
        }

        public IActionResult Page(string Name)
        {
            var page = pages.All().Where(x => x.Name == Name).FirstOrDefault();

            return View(page);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
