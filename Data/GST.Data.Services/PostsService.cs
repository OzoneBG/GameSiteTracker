namespace GST.Data.Services
{
    using System;
    using System.Linq;
    using Models;
    using Interfaces;
    using Common.Repository;

    public class PostsService : IPostsService
    {
        private readonly IDeletableEntityRepository<Post> posts;

        public PostsService(IDeletableEntityRepository<Post> posts)
        {
            this.posts = posts;
        }

        public IQueryable<Post> GetAllPosts()
        {
            var allPosts = posts.All();

            return allPosts;
        }
    }
}
