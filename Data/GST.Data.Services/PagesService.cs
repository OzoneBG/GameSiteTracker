namespace GST.Data.Services
{
    using Interfaces;
    using System.Linq;
    using Models;
    using Common.Repository;
    using System.Collections.Generic;

    public class PagesService : IPagesService
    {
        private readonly IDeletableEntityRepository<Page> pages;

        public PagesService(IDeletableEntityRepository<Page> pages)
        {
            this.pages = pages;
        }

        public IQueryable<Page> GetPageFor(string Name)
        {
            return pages.All().Where(x => x.Name == Name);
        }

        public bool ShouldCreateDefaultPages()
        {
            bool bShouldCreate;

            var allPages = pages.All().ToList();

            if (allPages.Count == 0)
            {
                bShouldCreate = true;
            }
            else
            {
                bShouldCreate = false;
            }

            return bShouldCreate;

        }

        public void AddDefaultPages(List<Page> pageList)
        {
            foreach (var page in pageList)
            {
                pages.Add(page);
            }

            pages.SaveChanges();
        }
    }
}
