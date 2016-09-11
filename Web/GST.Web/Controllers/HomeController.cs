namespace GST.Web.Controllers
{
    using GST.Data;
    using GST.Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using System;

    public class HomeController : Controller
    {
        GSTDbContext context;

        public HomeController(GSTDbContext context)
        {
            this.context = context;
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
            ViewData["Message"] = "Your contact page.";

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

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
