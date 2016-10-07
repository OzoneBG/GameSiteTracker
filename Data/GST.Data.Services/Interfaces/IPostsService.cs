using GST.Data.Models;
using System.Linq;

namespace GST.Data.Services.Interfaces
{
    public interface IPostsService
    {
        IQueryable<Post> GetAllPosts();
        void DeletePost(int id);
        IQueryable<Post> GetPostWithId(int id);
        void EditPost(int id, string title, string content);
        void AddPost(string title, string content, string id);
    }
}
