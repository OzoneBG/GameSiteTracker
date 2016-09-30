namespace GST.Data.Services
{
    using System.Linq;
    using Models;
    using Interfaces;
    using Common.Repository;
    using Microsoft.AspNetCore.Identity;
    using System;

    public class UsersService : IUsersService
    {
        private readonly IDeletableEntityRepository<User> users;
        private readonly UserManager<User> userManager;

        public UsersService(IDeletableEntityRepository<User> users, UserManager<User> userManager)
        {
            this.users = users;
            this.userManager = userManager;
        }

        public void DeleteUser(string guid)
        {
            var user = users.All().Where(x => x.Id == guid).FirstOrDefault();

            user.IsDeleted = true;
            user.DeletedOn = DateTime.Now;

            users.SaveChanges();
        }

        public IQueryable<User> GetAllUsers()
        {
            return users.All();
        }
    }
}
