namespace GST.Web.ViewModels.AccountViewModels
{
    using Data.Models;
    using Common.Mapping;

    public class ViewUsersViewModel : IMapFrom<User>
    {
        public string Id { get; set; }

        public string UserName { get; set; }
    }
}
