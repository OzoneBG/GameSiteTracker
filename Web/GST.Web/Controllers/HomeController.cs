namespace GST.Web.Controllers
{
    using Common;
    using Common.Mapping;
    using GST.Data.Models;
    using GST.Data.Services.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;
    using ViewModels.PagesViewModel;
    using ViewModels.PicturesViewModels;
    using ViewModels.VideosViewModels;
    using System;

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

        private int MaxPerPage
        {
            get
            {
                return GlobalConstants.MaxMediaPerPage;
            }
        }

        public IActionResult Index()
        {
            //Need news service

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
