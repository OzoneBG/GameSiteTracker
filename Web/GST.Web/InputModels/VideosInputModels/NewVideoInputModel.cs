namespace GST.Web.InputModels.VideosInputModels
{
    using System.ComponentModel.DataAnnotations;

    public class NewVideoInputModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string VideoUrl { get; set; }
    }
}
