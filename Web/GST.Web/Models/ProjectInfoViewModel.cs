namespace GST.Web.Models
{
    public class ProjectInfoViewModel
    {
        public int TotalDownloads { get; set; }

        public string BuildVersion { get; set; }

        public int DaysTillArrival { get; set; }

        public int TotalTasksForBuild { get; set; }

        public int TotalFinishedTasksForBuild { get; set; }

    }
}
