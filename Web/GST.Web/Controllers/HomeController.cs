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

        public IActionResult Index()
        {
            //Need news service

            return View();
        }

        public IActionResult Videos(int? p)
        {
            int page = 1;
            if (p == null)
            {
                page = 1;
            }
            else if (p <= 0)
            {
                RedirectToAction("PaginationTest", new { p = 1 });
            }
            else
            {
                page = (int)p;
            }

            var allVideos = videosService.GetAllVideos();

            ViewBag.TotalLinksToDisplay = GetLinksCountFor(allVideos.Count());
            ViewBag.CurrentPage = page;

            int maxPerPage = GlobalConstants.MaxMediaPerPage;
            int toSkip = (page * maxPerPage) - maxPerPage;

            var videosList = allVideos.Skip(toSkip).Take(maxPerPage).To<VideosViewModel>().ToList();

            return View(videosList);
        }

        public IActionResult Pictures(int? p)
        {
            int page = 1;
            if (p == null)
            {
                page = 1;
            }
            else if (p <= 0)
            {
                RedirectToAction("PaginationTest", new { p = 1 });
            }
            else
            {
                page = (int)p;
            }

            var pics = picturesService.GetAllPictures();

            ViewBag.TotalLinksToDisplay = GetLinksCountFor(pics.Count());
            ViewBag.CurrentPage = page;

            int maxPerPage = GlobalConstants.MaxMediaPerPage;
            int toSkip = (page * maxPerPage) - maxPerPage;

            var picsList = pics.Skip(toSkip).Take(maxPerPage).To<PicturesViewModel>().ToList();

            return View(picsList);
        }

        public IActionResult Page(string Name)
        {
            var page = pagesService.GetPageFor(Name).To<PageViewModel>().FirstOrDefault();

            return View(page);
        }


        #region Helpers
        private static int GetLinksCountFor(int totalItems)
        {
            return (int)Math.Ceiling(totalItems / (float)GlobalConstants.MaxMediaPerPage);
        }
        #endregion

        public IActionResult Error()
        {
            return View();
        }
    }
}
