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

        void EditStaticPage(string name, string newContent);
    }
}
