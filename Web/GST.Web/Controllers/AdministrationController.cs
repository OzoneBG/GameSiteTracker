namespace GST.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class AdministrationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
