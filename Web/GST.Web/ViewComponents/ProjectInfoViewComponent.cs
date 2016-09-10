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
                BuildVersion = "1039b",
                DaysTillArrival = 12,
                TotalTasksForBuild = 10,
                TotalFinishedTasksForBuild = 5
            };

            tempModel.BuildVersion = 'v' + tempModel.BuildVersion;

            return View(tempModel);
        }
    }
}
