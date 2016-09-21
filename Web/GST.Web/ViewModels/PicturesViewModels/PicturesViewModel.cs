namespace GST.Web.ViewModels.PicturesViewModels
{
    using Common.Mapping;
    using GST.Data.Models;

    public class PicturesViewModel : IMapFrom<Picture>
    {
        public string Name { get; set; }

        public string UrlToImage { get; set; }
    }
}
