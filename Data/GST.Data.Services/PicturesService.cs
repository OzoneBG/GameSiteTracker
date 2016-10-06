namespace GST.Data.Services
{
    using System;
    using System.Linq;
    using Models;
    using Interfaces;
    using Common.Repository;
    using System.IO;

    public class PicturesService : IPicturesService
    {
        private readonly IDeletableEntityRepository<Picture> pictures;

        public PicturesService(IDeletableEntityRepository<Picture> pictures)
        {
            this.pictures = pictures;
        }

        public void AddPicture(string name, string fileName)
        {
            Picture pic = new Picture
            {
                Name = name,
                UrlToImage = Path.Combine("Images", fileName)
            };

            pictures.Add(pic);
            pictures.SaveChanges();
        }

        public void DeletePicture(int id)
        {
            var pic = pictures.All().Where(x => x.Id == id).FirstOrDefault();

            pic.IsDeleted = true;
            pic.DeletedOn = DateTime.Now;

            pictures.SaveChanges();
        }

        public IQueryable<Picture> GetAllPictures()
        {
            return pictures.All();
        }
    }
}
