namespace GST.Data.Services
{
    using Models;
    using Interfaces;
    using Common.Repository;
    using System.Linq;
    using System;

    public class VideosService : IVideosService
    {
        private readonly IDeletableEntityRepository<Video> videos;

        public VideosService(IDeletableEntityRepository<Video> videos)
        {
            this.videos = videos;
        }

        public void AddNewVideo(string name, string videoUrl)
        {
            Video vid = new Video
            {
                Name = name,
                VideoUrl = videoUrl
            };

            videos.Add(vid);
            videos.SaveChanges();
        }

        public void DeleteVideo(int id)
        {
            var vid = videos.All().Where(x => x.Id == id).FirstOrDefault();

            vid.IsDeleted = true;
            vid.DeletedOn = DateTime.Now;

            videos.SaveChanges();
        }

        public IQueryable<Video> GetAllVideos()
        {
            return videos.All();
        }

        public string GetVideoName(int id)
        {
            return this.GetAllVideos().Where(x => x.Id == id).FirstOrDefault().Name;
        }
    }
}
