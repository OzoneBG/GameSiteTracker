namespace GST.Web.ViewModels.PicturesViewModels
{
    using GST.Data.Models;
    using GST.Web.Common.Mapping;

    public class AdministrationPicturesViewModel : PicturesViewModel, IMapFrom<Picture>
    {
        public int Id { get; set; }
    }
}
