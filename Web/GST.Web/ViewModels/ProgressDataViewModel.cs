namespace GST.Web.ViewModels
{
    public class ProgressDataViewModel
    {
        public int TotalDownloads { get; set; }

        public string LatestBuild { get; set; }

        public int ETA { get; set; }

        public int CurrentVersionTasks { get; set; }

        public int CompletedTasks { get; set; }
    }
}
