namespace GST.Web.Models
{
    public class ProjectInfoViewModel
    {
        public int TotalDownloads { get; set; }

        public string Version { get; set; }

        public int DaysTillArrival { get; set; }

        public int TotalTasksForVersion{ get; set; }

        public int TotalFinishedTasksForVersion{ get; set; }

    }
}
