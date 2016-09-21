namespace GST.Web.ViewComponents
{
    using GST.Data.Common.Repository;
    using GST.Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;

    public class PagesViewComponent : ViewComponent
    {
        private readonly IDeletableEntityRepository<Page> pages;

        public PagesViewComponent(IDeletableEntityRepository<Page> pages)
        {
            this.pages = pages;
        }

        public IViewComponentResult Invoke()
        {
            var allPages = pages.All().AsQueryable();

            return View(allPages);
        }
    }
}
