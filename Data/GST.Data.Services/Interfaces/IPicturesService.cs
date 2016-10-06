namespace GST.Data.Services.Interfaces
{
    using Models;
    using System.Linq;

    public interface IPicturesService
    {
        IQueryable<Picture> GetAllPictures();
        void AddPicture(string name, string path);
        void DeletePicture(int id);
    }
}
