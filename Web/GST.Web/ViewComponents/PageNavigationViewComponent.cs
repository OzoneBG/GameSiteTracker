namespace GST.Web.ViewComponents
{
    using Microsoft.AspNetCore.Mvc;

    public class PageNavigationViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(int currentPage, int totalLinksToDisplay, string redirectActionName, string controllerName)
        {
            ViewData["currentPage"] = currentPage;
            ViewData["totalLinksToDisplay"] = totalLinksToDisplay;
            ViewData["redirectActionName"] = redirectActionName;

            if (!string.IsNullOrWhiteSpace(controllerName))
            {
                ViewData["controllerName"] = controllerName;
            }
            else
            {
                ViewData["controllerName"] = "Home";
            }

            return View();
        }
    }
}
