namespace GST.Data.Services.Interfaces
{
    using Models;
    using System.Linq;

    public interface IVideosService
    {
        IQueryable<Video> GetAllVideos();
        void AddNewVideo(string name, string videoUrl);
        void DeleteVideo(int id);
        string GetVideoName(int id);
    }
}
