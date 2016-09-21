namespace GST.Web.ViewModels.VideosViewModels
{
    using GST.Data.Models;
    using Common.Mapping;

    public class VideosViewModel : IMapFrom<Video>
    {
        public string Name { get; set; }

        public string VideoUrl { get; set; }
    }
}
