namespace GST.Web.ViewModels.ViewComponentViewModels
{
    public class ProjectInfoViewModel
    {
        public int TotalDownloads { get; set; }

        public string LatestBuild { get; set; }

        public int ETA { get; set; }

        public int CurrentVersionTasks { get; set; }

        public int CompletedTasks { get; set; }
    }
}
