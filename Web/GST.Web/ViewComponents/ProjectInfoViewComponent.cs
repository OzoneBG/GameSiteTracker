namespace GST.Web.ViewComponents
{
    using Microsoft.AspNetCore.Mvc;
    using Models;

    public class ProjectInfoViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            ProjectInfoViewModel tempModel = new ProjectInfoViewModel
            {
                TotalDownloads = 23,
                Version = "1039b",
                DaysTillArrival = 12,
                TotalTasksForVersion = 10,
                TotalFinishedTasksForVersion = 5
            };

            tempModel.Version = 'v' + tempModel.Version;

            return View(tempModel);
        }
    }
}
