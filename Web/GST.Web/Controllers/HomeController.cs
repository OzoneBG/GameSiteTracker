namespace GST.Web.Controllers
{
    using Common;
    using Common.Mapping;
    using GST.Data.Services.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using ViewModels.PagesViewModel;
    using ViewModels.PicturesViewModels;
    using ViewModels.VideosViewModels;
    using System;
    using GST.Data.Models;
    using GST.Data.Common.Repository;

    public class HomeController : Controller
    {
        private readonly IVideosService videosService;
        private readonly IPicturesService picturesService;
        private readonly IPagesService pagesService;

        private readonly IDeletableEntityRepository<Post> _posts;

        public HomeController(IVideosService videosService, IPicturesService picturesService, IPagesService pagesService, IDeletableEntityRepository<Post> posts)
        {
            this.videosService = videosService;
            this.picturesService = picturesService;
            this.pagesService = pagesService;
            _posts = posts;
        }

        private int MaxPerPage
        {
            get
            {
                return GlobalConstants.MaxMediaPerPage;
            }
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Pesho()
        {
            //Need Posts service
            Post post1 = new Post()
            {
                Title = "Development Diary #1",
                Content = "1"
            };
            
            Post post2 = new Post()
            {
                Title = "Development Diary #2",
                Content = "2"
            };
            
            Post post3 = new Post()
            {
                Title = "Development Diary #3",
                Content = "3"
            };
            
            _posts.Add(post1);
            _posts.Add(post2);
            _posts.Add(post3);
            
            _posts.SaveChanges();

            return View();
            
        }

        public IActionResult Videos(int? p)
        {
            int page = PageChecks(p, "Videos");

            var allVideos = videosService.GetAllVideos();

            ViewBag.TotalLinksToDisplay = GetLinksCountFor(allVideos.Count());
            ViewBag.CurrentPage = page;

            int toSkip = GetPaginationDataToSkip(page);

            var videosList = allVideos.Skip(toSkip).Take(MaxPerPage).To<VideosViewModel>().ToList();

            return View(videosList);
        }

        public IActionResult Pictures(int? p)
        {
            int page = PageChecks(p, "Pictures");

            var allPics = picturesService.GetAllPictures();

            ViewBag.TotalLinksToDisplay = GetLinksCountFor(allPics.Count());
            ViewBag.CurrentPage = page;

            int toSkip = GetPaginationDataToSkip(page);

            var picsList = allPics.Skip(toSkip).Take(MaxPerPage).To<PicturesViewModel>().ToList();

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
            return (page * MaxPerPage) - MaxPerPage;
        }
        #endregion

        public IActionResult Error()
        {
            return View();
        }
    }
}
