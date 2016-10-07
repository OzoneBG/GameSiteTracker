namespace GST.Data.Services
{
    using System.Linq;
    using Models;
    using Interfaces;
    using Common.Repository;
    using System;

    public class PostsService : IPostsService
    {
        private readonly IDeletableEntityRepository<Post> posts;

        public PostsService(IDeletableEntityRepository<Post> posts)
        {
            this.posts = posts;
        }

        public void AddPost(string title, string content, string id)
        {
            Post post = new Post
            {
                Title = title,
                Content = content,
                AuthorId = id
            };

            posts.Add(post);

            posts.SaveChanges();
        }

        public void DeletePost(int id)
        {
            var post = GetPostWithId(id).FirstOrDefault();

            post.IsDeleted = true;
            post.DeletedOn = DateTime.Now;

            posts.SaveChanges();
        }

        public void EditPost(int id, string title, string content)
        {
            var post = GetPostWithId(id).FirstOrDefault();

            post.Title = title;
            post.Content = content;
            post.ModifiedOn = DateTime.Now;

            posts.SaveChanges();
        }

        public IQueryable<Post> GetAllPosts()
        {
            var allPosts = posts.All();

            return allPosts;
        }

        public IQueryable<Post> GetPostWithId(int id)
        {
            return GetAllPosts().Where(x => x.Id == id);
        }
    }
}
