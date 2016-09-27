namespace GST.Web.InputModels.PagesInputModels
{
    using System.ComponentModel.DataAnnotations;

    public class EditPageInputModel
    {
        public string PageName { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
