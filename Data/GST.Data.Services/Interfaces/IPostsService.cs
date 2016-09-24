using GST.Data.Models;
using System.Linq;

namespace GST.Data.Services.Interfaces
{
    public interface IPostsService
    {
        IQueryable<Post> GetAllPosts();
    }
}
