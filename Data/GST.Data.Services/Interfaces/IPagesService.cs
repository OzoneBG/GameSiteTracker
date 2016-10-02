namespace GST.Data.Services.Interfaces
{
    using Models;
    using System.Collections.Generic;
    using System.Linq;

    public interface IPagesService
    {
        IQueryable<Page> GetPageFor(string Name);

        List<string> GetAllPageNames();

        bool ShouldCreateDefaultPages();

        void AddDefaultPages(List<Page> pageList);

        void EditStaticPage(string oldName, string newName, string newContent);

        string GetFirstPageName();

        void DeletePage(int id);

        void CreateNewPage(string name, string content, User author);
        string GetPageNameById(int pageId);
    }
}
