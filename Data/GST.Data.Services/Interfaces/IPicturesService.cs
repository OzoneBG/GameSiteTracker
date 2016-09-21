namespace GST.Data.Services.Interfaces
{
    using Models;
    using System.Linq;

    public interface IPicturesService
    {
        IQueryable<Picture> GetAllPictures();
    }
}
