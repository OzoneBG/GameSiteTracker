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
    using System;
    using ViewModels.PostsViewModels;

    public class HomeController : Controller
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

        private int MaxMediaPerPage
        {
            get
            {
                return GlobalConstants.MaxMediaPerPage;
            }
        }

        private int MaxPostsPerPage
        {
            get
            {
                return GlobalConstants.MaxPostsPerPage;
            }
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

            ViewBag.TotalLinksToDisplay = GetLinksCountFor(allVideos.Count());
            ViewBag.CurrentPage = page;

            int toSkip = GetPaginationDataToSkip(page);

            var videosList = allVideos.Skip(toSkip).Take(MaxMediaPerPage).To<VideosViewModel>().ToList();

            return View(videosList);
        }

        public IActionResult Pictures(int? p)
        {
            int page = PageChecks(p, "Pictures");

            var allPics = picturesService.GetAllPictures();

            ViewBag.TotalLinksToDisplay = GetLinksCountFor(allPics.Count());
            ViewBag.CurrentPage = page;

            int toSkip = GetPaginationDataToSkip(page);

            var picsList = allPics.Skip(toSkip).Take(MaxMediaPerPage).To<PicturesViewModel>().ToList();

            return View(picsList);
        }

        public IActionResult Page(string Name)
        {
            var page = pagesService.GetPageFor(Name).To<PageViewModel>().FirstOrDefault();

            return View(page);
        }


        #region Helpers
        private int GetLinksCountFor(int totalItems)
        {
            return (int)Math.Ceiling(totalItems / (float)GlobalConstants.MaxMediaPerPage);
        }

        private int PageChecks(int? page, string RedirectActionName)
        {
            if (page == null)
            {
                return 1;
            }
            else if (page <= 0)
            {
                RedirectToAction(RedirectActionName, new { p = 1 });
                return 0;
            }
            else
            {
                return (int)page;
            }
        }
        
        private int GetPaginationDataToSkip(int page)
        {
            return (page * MaxMediaPerPage) - MaxMediaPerPage;
        }
        #endregion

        public IActionResult Error()
        {
            return View();
        }
    }
}
