namespace GST.Data.Services
{
    using Models;
    using Interfaces;
    using Common.Repository;
    using System.Linq;

    public class VideosService : IVideosService
    {
        private readonly IDeletableEntityRepository<Video> videos;

        public VideosService(IDeletableEntityRepository<Video> videos)
        {
            this.videos = videos;
        }

        public IQueryable<Video> GetAllVideos()
        {
            return videos.All();
        }
    }
}
