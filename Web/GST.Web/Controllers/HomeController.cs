namespace GST.Web.Controllers
{
    using Common.Mapping;
    using GST.Data.Services.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using ViewModels.PagesViewModel;
    using ViewModels.PicturesViewModels;
    using ViewModels.VideosViewModels;

    public class HomeController : Controller
    {
        private readonly IVideosService videosService;
        private readonly IPicturesService picturesService;
        private readonly IPagesService pagesService;

        public HomeController(IVideosService videosService, IPicturesService picturesService, IPagesService pagesService)
        {
            this.videosService = videosService;
            this.picturesService = picturesService;
            this.pagesService = pagesService;
        }

        public IActionResult Index()
        {
            //Need news service

            return View();
        }

        public IActionResult Videos()
        {
            var videosList = videosService.GetAllVideos().To<VideosViewModel>().ToList();

            return View(videosList);
        }

        public IActionResult Pictures()
        {
            var picturesList = picturesService.GetAllPictures().To<PicturesViewModel>().ToList();

            return View(picturesList);
        }

        public IActionResult Page(string Name)
        {
            var page = pagesService.GetPageFor(Name).To<PageViewModel>().FirstOrDefault();

            return View(page);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
