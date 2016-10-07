namespace GST.Web.ViewModels.PostsViewModels
{
    using Data.Models;
    using Common.Mapping;
    using System;

    public class ListPostsViewModel : IMapFrom<Post>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public User Author { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
