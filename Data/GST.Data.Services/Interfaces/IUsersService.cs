namespace GST.Data.Services.Interfaces
{
    using Models;
    using System.Linq;

    public interface IUsersService
    {
        IQueryable<User> GetAllUsers();
        void DeleteUser(string guid);
    }
}
