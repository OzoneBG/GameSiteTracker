namespace GST.Data.Services.Interfaces
{
    using Models;
    using System.Linq;

    public interface IPagesService
    {
        IQueryable<Page> GetPageFor(string Name);
    }
}
