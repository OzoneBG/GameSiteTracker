namespace GST.Web.Controllers
{
    using Common;
    using Common.Mapping;
    using Data.Services.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using ViewModels.PagesViewModel;
    using ViewModels.PicturesViewModels;
    using ViewModels.VideosViewModels;
    using ViewModels.PostsViewModels;
    using Newtonsoft.Json;

    public class HomeController : BaseController
    {
        private readonly IVideosService videosService;
        private readonly IPicturesService picturesService;
        private readonly IPagesService pagesService;
        private readonly IPostsService postsService;

        public HomeController(IVideosService videosService, 
                            IPicturesService picturesService, 
                            IPagesService pagesService, 
                            IPostsService postsService)
        {
            this.videosService = videosService;
            this.picturesService = picturesService;
            this.pagesService = pagesService;
            this.postsService = postsService;
        }

        public IActionResult Index(int? p)
        {
            int page = PageChecks(p, "Index");

            var allPosts = postsService.GetAllPosts();

            ViewBag.TotalLinksToDisplay = GetLinksCountFor(allPosts.Count());
            ViewBag.CurrentPage = page;

            int toSkip = GetPaginationDataToSkip(page);

            var videosList = allPosts.Skip(toSkip).Take(MaxMediaPerPage).To<PostsViewModel>().ToList();

            return View(videosList);
        }

        public IActionResult Videos(int? p)
        {
            int page = PageChecks(p, "Videos");

            var allVideos = videosService.GetAllVideos();

            var vidList = allVideos
                .Skip(GetSkipCount(allVideos.Count(), page))
                .Take(MaxMediaPerPage).To<VideosViewModel>()
                .ToList();

            return View(vidList);
        }

        public IActionResult Pictures(int? p)
        {
            int page = PageChecks(p, "Pictures");

            var allPics = picturesService.GetAllPictures();

            var picsList = allPics
                .Skip(GetSkipCount(allPics.Count(), page))
                .Take(MaxMediaPerPage).To<PicturesViewModel>()
                .ToList();

            return View(picsList);
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
