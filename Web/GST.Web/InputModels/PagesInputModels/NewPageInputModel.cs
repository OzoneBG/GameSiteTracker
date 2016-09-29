namespace GST.Web.InputModels.PagesInputModels
{
    using System.ComponentModel.DataAnnotations;

    public class NewPageInputModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
