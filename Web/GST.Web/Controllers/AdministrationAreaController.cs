namespace GST.Web.Controllers
{
    using Data.Services.Interfaces;

    public class AdministrationAreaController : AdministratorController
    {
        public AdministrationAreaController(IPagesService pageService, IUsersService usersService) : base(pageService, usersService)
        {
        }
    }
}
