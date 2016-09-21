namespace GST.Data.Services
{
    using Interfaces;
    using System.Linq;
    using Models;
    using Common.Repository;

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
    }
}
