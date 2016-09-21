namespace GST.Data.Services.Interfaces
{
    using Models;
    using System.Linq;

    public interface IVideosService
    {
        IQueryable<Video> GetAllVideos();
    }
}
