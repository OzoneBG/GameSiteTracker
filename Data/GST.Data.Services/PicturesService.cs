namespace GST.Data.Services
{
    using System;
    using System.Linq;
    using Models;
    using Interfaces;
    using Common.Repository;

    public class PicturesService : IPicturesService
    {
        private readonly IDeletableEntityRepository<Picture> pictures;

        public PicturesService(IDeletableEntityRepository<Picture> pictures)
        {
            this.pictures = pictures;
        }

        public IQueryable<Picture> GetAllPictures()
        {
            return pictures.All();
        }
    }
}
