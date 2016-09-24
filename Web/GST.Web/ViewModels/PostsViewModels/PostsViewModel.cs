namespace GST.Web.ViewModels.PostsViewModels
{
    using Data.Models;
    using Common.Mapping;
    using System;

    public class PostsViewModel : IMapFrom<Post>
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public User Author { get; set; }
    }
}
