namespace GST.Web.InputModels.PicturesInputModels
{
    using Microsoft.AspNetCore.Http;

    public class NewPictureInputModel
    {
        public string Name { get; set; }

        public IFormFile File { get; set; }
    }
}
