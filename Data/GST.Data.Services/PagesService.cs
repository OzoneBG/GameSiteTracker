namespace GST.Data.Services
{
    using Interfaces;
    using System.Linq;
    using Models;
    using Common.Repository;
    using System.Collections.Generic;
    using System;

    public class PagesService : IPagesService
    {
        private readonly IDeletableEntityRepository<Page> pages;

        public PagesService(IDeletableEntityRepository<Page> pages)
        {
            this.pages = pages;
        }

        public IQueryable<Page> GetPageFor(string Name)
        {
            var page = pages.All().Where(x => x.Name == Name);

            return page;
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

        public List<string> GetAllPageNames()
        {
            var allPageNames = pages.All().Select(x => x.Name).ToList();

            return allPageNames;
        }

        public void EditStaticPage(string oldName, string newName, string newContent)
        {
            var page = GetPageFor(oldName).FirstOrDefault();

            page.Name = newName;
            page.Content = newContent;
            page.ModifiedOn = DateTime.Now;

            pages.SaveChanges();
        }

        public string GetFirstPageName()
        {
            string pageName = pages.All().Select(x => x.Name).FirstOrDefault();

            return pageName;
        }

        public void DeletePage(int id)
        {
            var page = pages.All().Where(x => x.Id == id).FirstOrDefault();

            page.DeletedOn = DateTime.Now;
            page.IsDeleted = true;

            pages.SaveChanges();
        }

        public void CreateNewPage(string name, string content, User author)
        {
            Page page = new Page
            {
                Name = name,
                Content = content,
                AuthorId = author.Id
            };

            pages.Add(page);
            pages.SaveChanges();
        }

        public string GetPageNameById(int pageId)
        {
            return pages.All().Where(x => x.Id == pageId).FirstOrDefault().Name;
        }
    }
}
