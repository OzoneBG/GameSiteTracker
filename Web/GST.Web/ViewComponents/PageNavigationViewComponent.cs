namespace GST.Web.ViewComponents
{
    using Microsoft.AspNetCore.Mvc;

    public class PageNavigationViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(int currentPage, int totalLinksToDisplay, string redirectActionName)
        {
            ViewData["currentPage"] = currentPage;
            ViewData["totalLinksToDisplay"] = totalLinksToDisplay;
            ViewData["redirectActionName"] = redirectActionName;

            return View();
        }
    }
}
