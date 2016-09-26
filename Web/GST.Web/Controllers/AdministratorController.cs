namespace GST.Web.Controllers
{
    using Data.Services.Interfaces;

    public abstract class AdministratorController : ModeratorController
    {
        public AdministratorController(IPagesService pageService) : base(pageService)
        {
        }
    }
}
