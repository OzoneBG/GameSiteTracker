namespace GST.Web.ViewModels.PostsViewModels
{
    using Data.Models;
    using Common.Mapping;

    public class EditPostViewModel : IMapFrom<Post>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }
    }
}
