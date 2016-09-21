namespace GST.Web.ViewModels.PagesViewModel
{
    using Common.Mapping;
    using GST.Data.Models;

    public class PageViewModel : IMapFrom<Page>
    {
        public string Name { get; set; }

        public string Content { get; set; }
    }
}
